namespace Project.Controllers;

using Microsoft.AspNetCore.Mvc;
using Project.Repositories.Interfaces;

[ApiController]
[Route("[controller]")]
public class ToolsController(IToolsRepository toolsRepository) : ControllerBase
{
    private readonly IToolsRepository toolsRepository = toolsRepository;

    [HttpGet]
    public async Task<IActionResult> GetTools()
    {
        var result = await this.toolsRepository.GetToolsAsync();
        if (!result.Success || result.Value == null)
        {
            return this.NotFound(result.Error);
        }

        return this.Ok(result.Value);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetTool([FromRoute] int id)
    {
        var result = await this.toolsRepository.GetByIdAsync(id);
        if (!result.Success || result.Value == null)
        {
            return this.NotFound(result.Error);
        }

        return this.Ok(result.Value);
    }
}
