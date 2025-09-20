namespace Project.Services;

using Project.Models.Core;
using Project.Models.Entities;
using Project.Repositories.Interfaces;
using Project.Services.Interfaces;

public class UsersService(IUsersRepository usersRepository) : IUsersService
{
    private readonly IUsersRepository usersRepository = usersRepository;

    public async Task<Result<List<UsersEntity>>> GetUsers()
    {
        var users = await this.usersRepository.GetUsersAsync();
        if (!users.Success)
        {
            return Result.Fail<List<UsersEntity>>(users.Error!);
        }

        return users;
    }

    public async Task<Result<UsersEntity>> GetUser(int id)
    {
        var user = await this.usersRepository.GetUserByIdAsync(id);
        if (!user.Success)
        {
            return Result.Fail<UsersEntity>(user.Error!);
        }

        return user;
    }

    public async Task<Result<UsersEntity>> CreateUser(UsersEntity user)
    {
        var newEntity = new UsersEntity
        {
            Name = user.Name,
            Email = user.Email,
            Password = user.Password,
        };

        var createdUser = await this.usersRepository.CreateUserAsync(newEntity);
        if (!createdUser.Success)
        {
            return Result.Fail<UsersEntity>(createdUser.Error!);
        }

        return createdUser;
    }

    public async Task<Result<UsersEntity>> UpdateUser(UsersEntity user)
    {
        var newEntity = new UsersEntity
        {
            Id = user.Id,
            Name = user.Name,
            Email = user.Email,
            Password = user.Password,
        };

        var updatedUser = await this.usersRepository.UpdateUserAsync(newEntity);
        if (!updatedUser.Success)
        {
            return Result.Fail<UsersEntity>(updatedUser.Error!);
        }

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
