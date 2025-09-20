namespace Project.Repositories.Interfaces;

using Project.Models.Core;
using Project.Models.Entities;

public interface IRecipesRepository
{
    public Task<Result<List<RecipesEntity>>> GetRecipesAsync(Pagination pagination);
    public Task<Result<RecipesEntity>> GetRecipeByIdAsync(int id);
    public Task<Result<RecipesEntity>> CreateRecipeAsync(RecipesEntity recipe);
    public Task<Result<RecipesEntity>> UpdateRecipeAsync(RecipesEntity recipe);
    public Task<Result> DeleteRecipesAsync(int id);

}
