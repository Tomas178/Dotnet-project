namespace Project.Repositories;

using Project.Database;
using Project.Repositories.Interfaces;
using Project.Models.Core;
using Project.Models.Entities;
using Microsoft.EntityFrameworkCore;

public class RecipesIngredientsRepository(ProjectDbContext dbContext) : IRecipesIngredientsRepository
{
    private readonly ProjectDbContext dbContext = dbContext;

    public async Task<Result<List<RecipesIngredientsEntity>>> CreateRecipesIngredientsLinksAsync(List<RecipesIngredientsEntity> links)
    {
        try
        {
            this.dbContext.AddRange(links);
            await this.dbContext.SaveChangesAsync();

            return Result.Ok(links);
        }
        catch (Exception ex)
        {
            return Result.Fail<List<RecipesIngredientsEntity>>(ex.Message);
        }
    }

    public async Task<Result> DeleteByRecipeIdAsync(int id)
    {
        try
        {
            var links = await this.dbContext.RecipesIngredients
                .Where(ri => ri.RecipeId == id)
                .ToListAsync();

            this.dbContext.RemoveRange(links);
            await this.dbContext.SaveChangesAsync();

            return Result.Ok();
        }
        catch (Exception ex)
        {
            return Result.Fail($"Failed to delete ingredients {ex.Message}");
        }
    }
}
