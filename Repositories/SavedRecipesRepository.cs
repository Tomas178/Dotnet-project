
namespace Project.Repositories;

using Project.Database;
using Project.Models.Core;
using Project.Models.Entities;
using Project.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

public class SavedRecipesRepository(ProjectDbContext dbContext) : ISavedRecipesRepository
{
    private readonly ProjectDbContext dbContext = dbContext;

    public async Task<Result<List<SavedRecipesEntity>>> GetSavedRecipesByUserAsync(int userId)
    {
        try
        {
            var links = await this.dbContext.SavedRecipes
            .Include(sr => sr.User)
            .Include(sr => sr.Recipe)
            .Where(sr => sr.UserId == userId)
            .OrderByDescending(sr => sr.CreatedAt)
            .ToListAsync();

            return Result.Ok(links);
        }
        catch (Exception ex)
        {
            return Result.Fail<List<SavedRecipesEntity>>(ex.Message);
        }
    }

    public async Task<Result<SavedRecipesEntity>> CreateSavedRecipeLinkAsync(SavedRecipesEntity link)
    {
        try
        {
            var existingLink = await this.dbContext.SavedRecipes
                .FirstOrDefaultAsync(sr => sr.UserId == link.UserId && sr.RecipeId == link.RecipeId);

            if (existingLink != null)
            {
                return Result.Fail<SavedRecipesEntity>($"Recipe {link.RecipeId} is already saved by user {link.UserId}");
            }

            this.dbContext.SavedRecipes.Add(link);
            await this.dbContext.SaveChangesAsync();

            return Result.Ok(link);
        }
        catch (Exception ex)
        {
            return Result.Fail<SavedRecipesEntity>(ex.Message);
        }
    }

    public async Task<Result> DeleteSavedRecipeLinkAsync(SavedRecipesEntity link)
    {
        try
        {
            var existingLink = await this.dbContext.SavedRecipes
                .FirstOrDefaultAsync(sr => sr.UserId == link.UserId && sr.RecipeId == link.RecipeId);

            if (existingLink is null)
            {
                return Result.Fail("Saved recipe does not exist");
            }

            this.dbContext.SavedRecipes.Remove(existingLink);
            await this.dbContext.SaveChangesAsync();

            return Result.Ok();
        }
        catch (Exception ex)
        {
            return Result.Fail($"Failed to delete saved recipe link: {ex.Message}");
        }
    }
}
