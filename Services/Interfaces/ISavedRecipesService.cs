namespace Project.Services.Interfaces;

using Project.Models.Core;
using Project.Models.Entities;

public interface ISavedRecipesService
{
    public Task<Result<SavedRecipesEntity>> CreateLink(SavedRecipesEntity link);
    public Task<Result> DeleteLink(SavedRecipesEntity link);
}
