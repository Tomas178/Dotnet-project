namespace Project.Repositories.Interfaces;

using Project.Models.Core;
using Project.Models.Entities;

public interface IUsersRepository
{
    public Task<Result<List<UsersEntity>>> GetUsersAsync();
    public Task<Result<UsersEntity>> GetUserByIdAsync(int id);
    public Task<Result<UsersEntity>> CreateUserAsync(UsersEntity user);
    public Task<Result<UsersEntity>> UpdateUserAsync(UsersEntity user);
    public Task<Result> DeleteUserAsync(int id);
}
