namespace Project.Controllers;

using Project.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Project.Models.Dtos.Users;

[ApiController]
[Route("api/[controller]")]
public class UsersController(IUsersService usersService) : ControllerBase
{


    private readonly IUsersService usersService = usersService;

    [HttpGet]
    public async Task<IActionResult> GetUsers()
    {
        var result = await this.usersService.GetUsers();
        if (!result.Success || result.Value is null || result.Value.Count == 0)
        {
            return this.NoContent();
        }

        return this.Ok(result.Value);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetUser([FromRoute] int id)
    {
        var result = await this.usersService.GetUser(id);
        if (!result.Success || result.Value is null)
        {
            return this.NotFound(result.Error);
        }

        return this.Ok(result.Value);
    }

    [HttpPost]
    public async Task<IActionResult> CreateUser([FromBody] CreateUsersRequestDto user)
    {
        var result = await this.usersService.CreateUser(user);
        if (!result.Success || result.Value is null)
        {
            return this.BadRequest(result.Error);
        }

        return this.Ok(result.Value);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateUser(UpdateUsersRequestDto user)
    {
        var result = await this.usersService.UpdateUser(user);
        if (!result.Success || result.Value is null)
        {
            return this.BadRequest(result.Error);
        }

        return this.Ok(result.Value);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUser([FromRoute] int id)
    {
        var result = await this.usersService.DeleteUser(id);
        if (!result.Success)
        {
            return this.BadRequest(result.Error);
        }

        return this.NoContent();
    }
}
