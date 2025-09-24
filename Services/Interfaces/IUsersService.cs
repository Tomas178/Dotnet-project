namespace Project.Services.Interfaces;

using Project.Models.Core;
using Project.Models.Dtos.Users;

public interface IUsersService
{
    public Task<Result<ICollection<UsersResponseDto>>> GetUsers();
    public Task<Result<UsersResponseDto>> GetUser(int id);
    public Task<Result<UsersResponseDto>> CreateUser(CreateUsersRequestDto user);
    public Task<Result<UsersResponseDto>> UpdateUser(UpdateUsersRequestDto user);
    public Task<Result> DeleteUser(int id);
}
