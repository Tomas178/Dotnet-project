namespace Project.Services;

using Project.Services.Interfaces;
using Project.Models.Core;
using Project.Models.Entities;
using Project.Repositories.Interfaces;

public class SavedRecipesService(
    ISavedRecipesRepository savedRecipesRepository,
    IRecipesService recipesService
    ) : ISavedRecipesService
{
    private readonly ISavedRecipesRepository savedRecipesRepository = savedRecipesRepository;
    private readonly IRecipesService recipesService = recipesService;

    public async Task<Result<SavedRecipesEntity>> CreateLink(SavedRecipesEntity link)
    {
        var recipeToBeSaved = await this.recipesService.GetRecipe(link.RecipeId);
        if (recipeToBeSaved.Value == null)
        {
            return Result.Fail<SavedRecipesEntity>("Recipe not found");
        }


        if (recipeToBeSaved.Value.UserId == link.UserId)
        {
            return Result.Fail<SavedRecipesEntity>("Author cannot save his own recipe");
        }

        var newEntity = new SavedRecipesEntity
        {
            RecipeId = link.RecipeId,
            UserId = link.UserId
        };

        var createdLink = await this.savedRecipesRepository.CreateSavedRecipeLinkAsync(newEntity);
        if (!createdLink.Success)
        {
            return Result.Fail<SavedRecipesEntity>(createdLink.Error!);
        }

        return createdLink;
    }

    public async Task<Result> DeleteLink(SavedRecipesEntity link)
    {
        var deletedLink = await this.savedRecipesRepository.DeleteSavedRecipeLinkAsync(link);
        if (!deletedLink.Success)
        {
            return Result.Fail<SavedRecipesEntity>(deletedLink.Error!);
        }

        return deletedLink;
    }
}
