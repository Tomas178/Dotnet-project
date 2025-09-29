namespace Project.Services;

using Project.Database;
using Project.Models.Core;
using Project.Models.Entities;
using Project.Models.Dtos.Recipes;
using Project.Repositories.Interfaces;
using Project.Services.Interfaces;
using Project.Utils;

public class RecipesService(
    IRecipesRepository recipesRepository,
    IIngredientsRepository ingredientsRepository,
    IRecipesIngredientsRepository recipesIngredientsRepository,
    IToolsRepository toolsRepository,
    IRecipesToolsRepository recipesToolsRepository,
    IUsersService usersService,
    ProjectDbContext dbContext
    ) : IRecipesService
{
    private readonly IRecipesRepository recipesRepository = recipesRepository;
    private readonly IIngredientsRepository ingredientsRepository = ingredientsRepository;
    private readonly IRecipesIngredientsRepository recipesIngredientsRepository = recipesIngredientsRepository;
    private readonly IToolsRepository toolsRepository = toolsRepository;
    private readonly IRecipesToolsRepository recipesToolsRepository = recipesToolsRepository;

    private readonly IUsersService usersService = usersService;
    private readonly ProjectDbContext dbContext = dbContext;

    public async Task<Result<ICollection<RecipesResponseDto>>> GetRecipes(Pagination pagination)
    {
        if (pagination.Offset < 0 || pagination.Limit <= 0)
        {
            return Result.Fail<ICollection<RecipesResponseDto>>("Pagination offset and limit should be greater than zero");
        }


        var result = await this.recipesRepository.GetRecipesAsync(pagination);
        if (!result.Success)
        {
            return Result.Fail<ICollection<RecipesResponseDto>>(result.Error!);
        }

        var recipes = Mapper.MapToResponseDto(result.Value!);

        return Result.Ok(recipes);
    }

    public async Task<Result<RecipesResponseDto>> GetRecipe(int id)
    {
        var result = await this.recipesRepository.GetRecipeByIdAsync(id);
        if (!result.Success)
        {
            return Result.Fail<RecipesResponseDto>(result.Error!);
        }

        var recipe = Mapper.MapToResponseDto(result.Value!);

        return Result.Ok(recipe);
    }

    public async Task<Result<RecipesResponseDto>> CreateRecipe(CreateRecipesRequestDto recipe)
    {
        try
        {
            var checkUser = await this.usersService.GetUser(recipe.UserId);
            if (!checkUser.Success || checkUser.Value == null)
            {
                return Result.Fail<RecipesResponseDto>(checkUser.Error!);
            }

            var newEntity = new RecipesEntity
            {
                Title = recipe.Title,
                Duration = recipe.Duration,
                Steps = string.Join('\n', recipe.Steps),
                UserId = checkUser.Value.Id
            };

            var result = await this.recipesRepository.CreateRecipeAsync(newEntity);
            if (!result.Success)
            {
                return Result.Fail<RecipesResponseDto>(result.Error!);
            }

            var recipeId = result.Value!.Id;

            await this.InsertIngredientsAsync(recipeId, recipe.Ingredients);
            await this.InsertToolsAsync(recipeId, recipe.Tools);

            await this.dbContext.SaveChangesAsync();

            result = await this.recipesRepository.GetRecipeByIdAsync(recipeId);

            var createdRecipe = Mapper.MapToResponseDto(result.Value!);
            return Result.Ok(createdRecipe);
        }
        catch (Exception ex)
        {
            return Result.Fail<RecipesResponseDto>($"Failed to create recipe: {ex.Message}");
        }
    }

    public async Task<Result<RecipesResponseDto>> UpdateRecipe(UpdateRecipesRequestDto recipe)
    {
        var checkUser = await this.usersService.GetUser(recipe.UserId);
        if (!checkUser.Success || checkUser.Value == null)
        {
            return Result.Fail<RecipesResponseDto>(checkUser.Error!);
        }
        ;

        var checkRecipe = await this.recipesRepository.GetRecipeByIdAsync(recipe.Id);
        if (!checkRecipe.Success || checkRecipe.Value == null)
        {
            return Result.Fail<RecipesResponseDto>(checkRecipe.Error!);
        }

        if (checkRecipe.Value.UserId != checkUser.Value.Id)
        {
            return Result.Fail<RecipesResponseDto>("You are not the author!");
        }

        var existingRecipe = checkRecipe.Value;

        existingRecipe.Title = recipe.Title;
        existingRecipe.Steps = string.Join('\n', recipe.Steps);
        existingRecipe.Duration = recipe.Duration;

        await this.recipesIngredientsRepository.DeleteByRecipeIdAsync(existingRecipe.Id);
        await this.InsertIngredientsAsync(existingRecipe.Id, recipe.Ingredients);


        await this.recipesToolsRepository.DeleteByRecipeIdAsync(existingRecipe.Id);
        await this.InsertToolsAsync(existingRecipe.Id, recipe.Tools);


        await this.dbContext.SaveChangesAsync();

        var updatedRecipe = await this.recipesRepository.GetRecipeByIdAsync(existingRecipe.Id);
        if (!updatedRecipe.Success || updatedRecipe.Value == null)
        {
            return Result.Fail<RecipesResponseDto>("Failed to update the recipe");
        }

        var dto = Mapper.MapToResponseDto(updatedRecipe.Value);
        return Result.Ok(dto);
    }


    public async Task<Result> DeleteRecipe(int id)
    {
        var checkRecipe = await this.recipesRepository.GetRecipeByIdAsync(id);
        if (!checkRecipe.Success || checkRecipe.Value == null)
        {
            return Result.Fail<RecipesResponseDto>(checkRecipe.Error!);
        }

        if (checkRecipe.Value.UserId != checkRecipe.Value.UserId)
        {
            return Result.Fail<RecipesResponseDto>("You are not the author!");
        }

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
