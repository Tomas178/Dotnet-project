namespace Project.Services.Interfaces;

using Project.Models.Core;
using Project.Models.Dtos.Recipes;

public interface IRecipesService
{
    public Task<Result<ICollection<RecipesResponseDto>>> GetRecipes(Pagination pagination);
    public Task<Result<RecipesResponseDto>> GetRecipe(int id);
    public Task<Result<RecipesResponseDto>> CreateRecipe(CreateRecipesRequestDto recipe);
    public Task<Result<RecipesResponseDto>> UpdateRecipe(UpdateRecipesRequestDto recipe);
    public Task<Result> DeleteRecipe(int recipeId, int userId);
}
