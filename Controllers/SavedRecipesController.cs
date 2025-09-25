namespace Project.Controllers;

using Microsoft.AspNetCore.Mvc;
using Project.Services.Interfaces;
using Project.Models.Entities;

[ApiController]
[Route("[controller]")]
public class SavedRecipesController(ISavedRecipesService savedRecipesService) : ControllerBase
{
    private readonly ISavedRecipesService savedRecipesService = savedRecipesService;

    public async Task<IActionResult> Save(SavedRecipesEntity link)
    {
        var result = await this.savedRecipesService.CreateLink(link);
        if (!result.Success || result.Value is null)
        {
            return this.NotFound(result.Error);
        }

        return this.NoContent();
    }

    public async Task<IActionResult> Unsave(SavedRecipesEntity link)
    {
        var result = await this.savedRecipesService.DeleteLink(link);
        if (!result.Success)
        {
            return this.BadRequest(result.Error);
        }

        return this.NoContent();
    }
}
