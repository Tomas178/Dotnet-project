namespace Project.Repositories;

using Microsoft.EntityFrameworkCore;
using Project.Database;
using Project.Models.Core;
using Project.Models.Entities;
using Project.Repositories.Interfaces;

public class IngredientsRepository(ProjectDbContext dbContext) : IIngredientsRepository
{
    private readonly ProjectDbContext dbContext = dbContext;

    public async Task<Result<List<IngredientsEntity>>> CreateIngredientsAsync(List<IngredientsEntity> ingredients)
    {
        try
        {
            this.dbContext.Ingredients.AddRange(ingredients);
            await this.dbContext.SaveChangesAsync();

            return Result.Ok(ingredients);
        }
        catch (Exception ex)
        {
            return Result.Fail<List<IngredientsEntity>>(ex.Message);
        }
    }

    public async Task<Result<IngredientsEntity>> GetByIdAsync(int id)
    {
        try
        {
            var ingredient = await this.dbContext.Ingredients.FindAsync(id);
            if (ingredient is null)
            {
                return Result.Fail<IngredientsEntity>($"Ingredient with id: {id} not found");
            }

            return Result.Ok(ingredient);
        }
        catch (Exception ex)
        {
            return Result.Fail<IngredientsEntity>(ex.Message);
        }
    }

    public async Task<Result<List<IngredientsEntity>>> GetByNamesAsync(List<string> names)
    {
        try
        {
            var ingredients = await this.dbContext.Ingredients.Where(t => names.Contains(t.Name)).ToListAsync();
            if (ingredients is null)
            {
                return Result.Fail<List<IngredientsEntity>>("Ingredients with given names have not been found");
            }

            return Result.Ok(ingredients);
        }
        catch (Exception ex)
        {
            return Result.Fail<List<IngredientsEntity>>(ex.Message);
        }
    }
}
