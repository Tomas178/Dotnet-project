namespace Project.Controllers;

using Microsoft.AspNetCore.Mvc;
using Project.Repositories.Interfaces;

[ApiController]
[Route("[controller]")]
public class IngredientsController(IIngredientsRepository ingredientsRepository) : ControllerBase
{
    private readonly IIngredientsRepository ingredientsRepository = ingredientsRepository;

    [HttpGet]
    public async Task<IActionResult> GetIngredients()
    {
        var result = await this.ingredientsRepository.GetIngredientsAsync();
        if (!result.Success || result.Value == null)
        {
            return this.NotFound(result.Error);
        }

        return this.Ok(result.Value);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetIngredient([FromRoute] int id)
    {
        var result = await this.ingredientsRepository.GetByIdAsync(id);
        if (!result.Success || result.Value == null)
        {
            return this.NotFound(result.Error);
        }

        return this.Ok(result.Value);
    }
}
