namespace Project.Services.Interfaces;

using Project.Models.Core;
using Project.Models.Dtos.Recipes;
using Project.Models.Entities;

public interface IRecipesService
{
    public Task<Result<List<RecipesEntity>>> GetRecipes(Pagination pagination);
    public Task<Result<RecipesEntity>> GetRecipe(int id);
    public Task<Result<RecipesEntity>> CreateRecipe(CreateRecipesRequestDto recipe);
    public Task<Result<RecipesEntity>> UpdateRecipe(RecipesEntity recipe);
    public Task<Result> DeleteRecipe(int id);
}
