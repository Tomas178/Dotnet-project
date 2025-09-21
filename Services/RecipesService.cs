namespace Project.Services;

using Project.Models.Core;
using Project.Models.Entities;
using Project.Repositories.Interfaces;
using Project.Services.Interfaces;

public class RecipesService(IRecipesRepository recipesRepository) : IRecipesService
{
    private readonly IRecipesRepository recipesRepository = recipesRepository;

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

    public async Task<Result<RecipesEntity>> CreateRecipe(RecipesEntity recipe)
    {
        var newEntity = new RecipesEntity
        {
            Title = recipe.Title,
            Duration = recipe.Duration,
            Steps = recipe.Steps,
            UserId = recipe.UserId
        };

        var createdRecipe = await this.recipesRepository.CreateRecipeAsync(newEntity);
        if (!createdRecipe.Success)
        {
            return Result.Fail<RecipesEntity>(createdRecipe.Error!);
        }

        return createdRecipe;
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
}
