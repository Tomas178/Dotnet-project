namespace Project.Repositories;

using Microsoft.EntityFrameworkCore;
using Project.Database;
using Project.Models.Core;
using Project.Models.Entities;
using Project.Repositories.Interfaces;

public class RecipesRepository(ProjectDbContext dbContext) : IRecipesRepository
{
    private readonly ProjectDbContext dbContext = dbContext;

    public async Task<Result<List<RecipesEntity>>> GetRecipesAsync(Pagination pagination)
    {
        try
        {
            if (pagination.Offset <= 0 || pagination.Limit <= 0)
            {
                return Result.Fail<List<RecipesEntity>>("Pagination offset and limit should be greater than zero");
            }

            var recipes = await this.dbContext.Recipes
                .OrderBy(r => r.Id)
                .Skip(pagination.Offset * pagination.Limit)
                .Take(pagination.Limit)
                .ToListAsync();

            return Result.Ok(recipes);
        }
        catch (Exception ex)
        {
            return Result.Fail<List<RecipesEntity>>(ex.Message);
        }
    }

    public async Task<Result<RecipesEntity>> GetRecipeByIdAsync(int id)
    {
        try
        {
            var recipe = await this.dbContext.Recipes.FindAsync(id);
            if (recipe is null)
            {
                return Result.Fail<RecipesEntity>($"Recipe with id: {id} not found");
            }

            return Result.Ok(recipe);
        }
        catch (Exception ex)
        {
            return Result.Fail<RecipesEntity>(ex.Message);
        }
    }

    public async Task<Result<RecipesEntity>> CreateRecipeAsync(RecipesEntity recipe)
    {
        try
        {
            this.dbContext.Recipes.Add(recipe);
            await this.dbContext.SaveChangesAsync();

            return Result.Ok(recipe);
        }
        catch (Exception ex)
        {
            return Result.Fail<RecipesEntity>(ex.Message);
        }
    }

    public async Task<Result<RecipesEntity>> UpdateRecipeAsync(RecipesEntity recipe)
    {
        try
        {
            var existingRecipe = await this.dbContext.Recipes.FindAsync(recipe.Id);
            if (existingRecipe is null)
            {
                return Result.Fail<RecipesEntity>($"Recipe with id: {recipe.Id} not found");
            }

            existingRecipe.Title = recipe.Title;
            existingRecipe.Duration = recipe.Duration;
            existingRecipe.Steps = recipe.Steps;
            existingRecipe.UpdatedAt = recipe.UpdatedAt;

            return Result.Ok(recipe);
        }
        catch (Exception ex)
        {
            return Result.Fail<RecipesEntity>($"Failed to update recipe {ex.Message}");
        }
    }

    public async Task<Result> DeleteRecipeAsync(int id)
    {
        try
        {
            var recipe = await this.dbContext.Recipes.FindAsync(id);
            if (recipe is null)
            {
                return Result.Fail($"Recipe with id: {id} not found");
            }

            this.dbContext.Remove(recipe);
            await this.dbContext.SaveChangesAsync();

            return Result.Ok();
        }
        catch (Exception ex)
        {
            return Result.Fail($"Failed to delete recipe {ex.Message}");
        }

    }
}
