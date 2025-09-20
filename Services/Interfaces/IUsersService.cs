namespace Project.Services.Interfaces;

using Project.Models.Core;
using Project.Models.Entities;

public interface IUsersService
{
    public Task<Result<List<UsersEntity>>> GetUsers();
    public Task<Result<UsersEntity>> GetUser(int id);
    public Task<Result<UsersEntity>> CreateUser(UsersEntity user);
    public Task<Result<UsersEntity>> UpdateUser(UsersEntity user);
    public Task<Result> DeleteUser(int id);
}
