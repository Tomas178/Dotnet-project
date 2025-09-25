namespace Project.Services.Interfaces;

using Project.Models.Core;
using Project.Models.Dtos.SavedRecipes;

public interface ISavedRecipesService
{
    public Task<Result<SavedRecipesResponseDto>> CreateLink(PostLinkRequestDto link);
    public Task<Result> DeleteLink(PostLinkRequestDto link);
}
