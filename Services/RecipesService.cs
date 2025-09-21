namespace Project.Services;

using Project.Database;
using Project.Models.Core;
using Project.Models.Entities;
using Project.Models.Dtos.Recipes;
using Project.Repositories.Interfaces;
using Project.Services.Interfaces;

public class RecipesService(
    IRecipesRepository recipesRepository,
    IIngredientsRepository ingredientsRepository,
    IRecipesIngredientsRepository recipesIngredientsRepository,
    IToolsRepository toolsRepository,
    IRecipesToolsRepository recipesToolsRepository,
    ProjectDbContext dbContext
    ) : IRecipesService
{
    private readonly IRecipesRepository recipesRepository = recipesRepository;
    private readonly IIngredientsRepository ingredientsRepository = ingredientsRepository;
    private readonly IRecipesIngredientsRepository recipesIngredientsRepository = recipesIngredientsRepository;
    private readonly IToolsRepository toolsRepository = toolsRepository;
    private readonly IRecipesToolsRepository recipesToolsRepository = recipesToolsRepository;
    private readonly ProjectDbContext dbContext = dbContext;

    public async Task<Result<List<RecipesEntity>>> GetRecipes(Pagination pagination)
    {
        if (pagination.Offset <= 0 || pagination.Limit <= 0)
        {
            return Result.Fail<List<RecipesEntity>>("Pagination offset and limit should be greater than zero");
        }


        var recipes = await this.recipesRepository.GetRecipesAsync(pagination);
        if (!recipes.Success)
        {
            return Result.Fail<List<RecipesEntity>>(recipes.Error!);
        }

        return recipes;
    }

    public async Task<Result<RecipesEntity>> GetRecipe(int id)
    {
        var recipe = await this.recipesRepository.GetRecipeByIdAsync(id);
        if (!recipe.Success)
        {
            return Result.Fail<RecipesEntity>(recipe.Error!);
        }

        return recipe;
    }

    public async Task<Result<RecipesEntity>> CreateRecipe(CreateRecipesRequestDto recipe)
    {
        using var transaction = await this.dbContext.Database.BeginTransactionAsync();

        try
        {
            var newEntity = new RecipesEntity
            {
                Title = recipe.Title,
                Duration = recipe.Duration,
                Steps = string.Join('\n', recipe.Steps),
                UserId = recipe.UserId
            };

            var createdRecipe = await this.recipesRepository.CreateRecipeAsync(newEntity);
            if (!createdRecipe.Success)
            {
                await transaction.RollbackAsync();
                return Result.Fail<RecipesEntity>(createdRecipe.Error!);
            }

            var recipeId = createdRecipe.Value!.Id;

            var insertTasks = new List<Task>
            {
                this.InsertIngredientsAsync(recipeId, recipe.Ingredients),
                this.InsertToolsAsync(recipeId, recipe.Tools)
            };

            await Task.WhenAll(insertTasks);

            await this.dbContext.SaveChangesAsync();
            await transaction.CommitAsync();

            return createdRecipe;
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            return Result.Fail<RecipesEntity>($"Failed to create recipe: {ex.Message}");
        }
    }

    public async Task<Result<RecipesEntity>> UpdateRecipe(RecipesEntity recipe)
    {
        var newEntity = new RecipesEntity
        {
            Id = recipe.Id,
            UserId = recipe.UserId,
            Title = recipe.Title,
            Duration = recipe.Duration,
            Steps = recipe.Steps,
        };

        var updatedRecipe = await this.recipesRepository.UpdateRecipeAsync(newEntity);
        if (!updatedRecipe.Success)
        {
            return Result.Fail<RecipesEntity>(updatedRecipe.Error!);
        }

        return updatedRecipe;
    }

    public async Task<Result> DeleteRecipe(int id)
    {
        var deletedRecipe = await this.recipesRepository.DeleteRecipeAsync(id);
        if (!deletedRecipe.Success)
        {
            return Result.Fail(deletedRecipe.Error!);
        }

        return deletedRecipe;
    }


    private async Task InsertIngredientsAsync(int recipeId, List<string> ingredientNames)
    {
        if (ingredientNames.Count == 0)
        {
            return;
        }

        var existingIngredients = await this.ingredientsRepository.GetByNamesAsync(ingredientNames);
        var existingIngredientNames = existingIngredients.Value!.Select(i => i.Name).ToHashSet();

        var newIngredientNames = ingredientNames
            .Where(name => !existingIngredientNames.Contains(name))
            .Distinct()
            .ToList();

        var createdIngredients = new List<IngredientsEntity>();
        if (newIngredientNames.Count != 0)
        {
            var newIngredientEntities = newIngredientNames.Select(name => new IngredientsEntity { Name = name }).ToList();
            var createResult = await this.ingredientsRepository.CreateIngredientsAsync(newIngredientEntities);

            if (createResult.Success)
            {
                createdIngredients = createResult.Value;
            }
        }

        var allIngredients = existingIngredients.Value!.Concat(createdIngredients!).ToList();

        var links = allIngredients.Select(ingredient => new RecipesIngredientsEntity
        {
            RecipeId = recipeId,
            IngredientId = ingredient.Id
        }).ToList();

        await this.recipesIngredientsRepository.CreateRecipesIngredientsLinksAsync(links);
    }

    private async Task InsertToolsAsync(int recipeId, List<string> toolNames)
    {
        if (toolNames.Count == 0)
        {
            return;
        }

        var existingTools = await this.toolsRepository.GetByNamesAsync(toolNames);
        var existingToolNames = existingTools.Value!.Select(t => t.Name).ToHashSet();

        var newToolNames = toolNames
            .Where(name => !existingToolNames.Contains(name))
            .Distinct()
            .ToList();

        var createdTools = new List<ToolsEntity>();
        if (newToolNames.Count != 0)
        {
            var newToolEntities = newToolNames.Select(name => new ToolsEntity { Name = name }).ToList();
            var createResult = await this.toolsRepository.CreateToolsAsync(newToolEntities);

            if (createResult.Success)
            {
                createdTools = createResult.Value;
            }
        }

        var allTools = existingTools.Value!.Concat(createdTools!).ToList();

        var links = allTools.Select(tool => new RecipesToolsEntity
        {
            RecipeId = recipeId,
            ToolId = tool.Id
        }).ToList();

        await this.recipesToolsRepository.CreateRecipesToolsLinksAsync(links);
    }
}
