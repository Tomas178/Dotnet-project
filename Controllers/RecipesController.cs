namespace Project.Controllers;

using Project.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Project.Models.Dtos.Recipes;
using Project.Models.Core;

[ApiController]
[Route("[controller]")]
public class RecipesController(IRecipesService recipesService) : ControllerBase
{
    private readonly IRecipesService recipesService = recipesService;

    [HttpGet]
    public async Task<IActionResult> GetRecipes(Pagination pagination)
    {
        var result = await this.recipesService.GetRecipes(pagination);
        if (!result.Success || result.Value is null || result.Value.Count == 0)
        {
            return this.NoContent();
        }

        return this.Ok(result.Value);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetRecipe([FromRoute] int id)
    {
        var result = await this.recipesService.GetRecipe(id);
        if (!result.Success || result.Value is null)
        {
            return this.NotFound(result.Error);
        }

        return this.Ok(result.Value);
    }

    [HttpPost]
    public async Task<IActionResult> CreateRecipe([FromBody] CreateRecipesRequestDto recipe)
    {
        var result = await this.recipesService.CreateRecipe(recipe);
        if (!result.Success || result.Value is null)
        {
            return this.BadRequest(result.Error);
        }

        return this.Ok(result.Value);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateRecipe([FromBody] UpdateRecipesRequestDto recipe)
    {
        var result = await this.recipesService.UpdateRecipe(recipe);
        if (!result.Success || result.Value is null)
        {
            return this.BadRequest(result.Error);
        }

        return this.Ok(result.Value);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteRecipe([FromRoute] int id)
    {
        var result = await this.recipesService.DeleteRecipe(id);
        if (!result.Success)
        {
            return this.BadRequest(result.Error);
        }

        return this.NoContent();
    }
}
