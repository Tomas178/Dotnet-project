namespace Project.Repositories;

using Project.Database;
using Project.Repositories.Interfaces;
using Project.Models.Core;
using Project.Models.Entities;

public class RecipesIngredientsRepository(ProjectDbContext dbContext) : IRecipesIngredientsRepository
{
    private readonly ProjectDbContext dbContext = dbContext;

    public async Task<Result<List<RecipesIngredientsEntity>>> CreatedRecipesIngredientsLinkAsync(List<RecipesIngredientsEntity> links)
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
}
