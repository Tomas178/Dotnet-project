namespace Project.Controllers;

using Microsoft.AspNetCore.Mvc;
using Project.Services.Interfaces;
using Project.Models.Dtos.SavedRecipes;

[ApiController]
[Route("[controller]")]
public class SavedRecipesController(ISavedRecipesService savedRecipesService) : ControllerBase
{
    private readonly ISavedRecipesService savedRecipesService = savedRecipesService;

    [HttpPost("save")]
    public async Task<IActionResult> Save(PostLinkRequestDto link)
    {
        var result = await this.savedRecipesService.CreateLink(link);
        if (!result.Success || result.Value is null)
        {
            return this.NotFound(result.Error);
        }

        return this.Ok(result.Value);
    }

    [HttpPost("unsave")]
    public async Task<IActionResult> Unsave(PostLinkRequestDto link)
    {
        var result = await this.savedRecipesService.DeleteLink(link);
        if (!result.Success)
        {
            return this.BadRequest(result.Error);
        }

        return this.NoContent();
    }
}
