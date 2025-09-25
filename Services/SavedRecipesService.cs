namespace Project.Services;

using Project.Services.Interfaces;
using Project.Models.Core;
using Project.Models.Entities;
using Project.Repositories.Interfaces;
using Project.Models.Dtos.SavedRecipes;
using Project.Utils;

public class SavedRecipesService(
    ISavedRecipesRepository savedRecipesRepository,
    IRecipesService recipesService
    ) : ISavedRecipesService
{
    private readonly ISavedRecipesRepository savedRecipesRepository = savedRecipesRepository;
    private readonly IRecipesService recipesService = recipesService;

    public async Task<Result<SavedRecipesResponseDto>> CreateLink(PostLinkRequestDto link)
    {
        var recipeToBeSaved = await this.recipesService.GetRecipe(link.RecipeId);
        if (recipeToBeSaved.Value == null)
        {
            return Result.Fail<SavedRecipesResponseDto>("Recipe not found");
        }


        if (recipeToBeSaved.Value.UserId == link.UserId)
        {
            return Result.Fail<SavedRecipesResponseDto>("Author cannot save his own recipe");
        }

        var newEntity = new SavedRecipesEntity
        {
            RecipeId = link.RecipeId,
            UserId = link.UserId
        };

        var createdLink = await this.savedRecipesRepository.CreateSavedRecipeLinkAsync(newEntity);
        if (!createdLink.Success)
        {
            return Result.Fail<SavedRecipesResponseDto>(createdLink.Error!);
        }

        var savedRecipe = Mapper.MapToResponseDto(createdLink.Value!);

        return savedRecipe;
    }

    public async Task<Result> DeleteLink(PostLinkRequestDto link)
    {
        var newEntity = new SavedRecipesEntity
        {
            RecipeId = link.RecipeId,
            UserId = link.UserId
        };

        var deletedLink = await this.savedRecipesRepository.DeleteSavedRecipeLinkAsync(newEntity);
        if (!deletedLink.Success)
        {
            return Result.Fail(deletedLink.Error!);
        }

        return deletedLink;
    }
}
