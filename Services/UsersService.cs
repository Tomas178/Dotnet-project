namespace Project.Services;

using Project.Models.Core;
using Project.Models.Dtos.Users;
using Project.Models.Entities;
using Project.Repositories.Interfaces;
using Project.Services.Interfaces;
using Project.Utils;

public class UsersService(IUsersRepository usersRepository) : IUsersService
{
    private readonly IUsersRepository usersRepository = usersRepository;

    public async Task<Result<ICollection<UsersResponseDto>>> GetUsers()
    {
        var result = await this.usersRepository.GetUsersAsync();
        if (!result.Success)
        {
            return Result.Fail<ICollection<UsersResponseDto>>(result.Error!);
        }

        var users = Mapper.MapToResponseDto(result.Value!);

        return Result.Ok(users);
    }

    public async Task<Result<UsersResponseDto>> GetUser(int id)
    {
        var result = await this.usersRepository.GetUserByIdAsync(id);
        if (!result.Success)
        {
            return Result.Fail<UsersResponseDto>(result.Error!);
        }

        var user = Mapper.MapToResponseDto(result.Value!, true);

        return user;
    }

    public async Task<Result<UsersResponseDto>> CreateUser(CreateUsersRequestDto user)
    {
        var newEntity = new UsersEntity
        {
            Name = user.Name,
            Email = user.Email,
            Password = user.Password,
        };

        var result = await this.usersRepository.CreateUserAsync(newEntity);
        if (!result.Success)
        {
            return Result.Fail<UsersResponseDto>(result.Error!);
        }

        var createdUser = Mapper.MapToResponseDto(result.Value!);

        return createdUser;
    }

    public async Task<Result<UsersResponseDto>> UpdateUser(UpdateUsersRequestDto user)
    {
        var newEntity = new UsersEntity
        {
            Id = user.Id,
            Name = user.Name,
            Email = user.Email,
            Password = user.Password,
        };

        var result = await this.usersRepository.UpdateUserAsync(newEntity);
        if (!result.Success)
        {
            return Result.Fail<UsersResponseDto>(result.Error!);
        }

        var updatedUser = Mapper.MapToResponseDto(result.Value!);

        return updatedUser;
    }

    public async Task<Result> DeleteUser(int id)
    {
        var deletedUser = await this.usersRepository.DeleteUserAsync(id);
        if (!deletedUser.Success)
        {
            return Result.Fail(deletedUser.Error!);
        }

        return deletedUser;
    }
}
