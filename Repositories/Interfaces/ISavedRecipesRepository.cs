namespace Project.Repositories.Interfaces;

using Project.Models.Core;
using Project.Models.Entities;

public interface ISavedRecipesRepository
{
    public Task<Result<List<SavedRecipesEntity>>> GetSavedRecipesByUserAsync(int userId);
    public Task<Result<SavedRecipesEntity>> CreateSavedRecipeLinkAsync(SavedRecipesEntity link);
    public Task<Result> DeleteSavedRecipeLinkAsync(SavedRecipesEntity link);
}
