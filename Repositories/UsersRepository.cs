
namespace Project.Repositories;

using Project.Database;
using Project.Models.Core;
using Project.Models.Entities;
using Project.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

public class UsersRepository(ProjectDbContext dbContext) : IUsersRepository
{
    private readonly ProjectDbContext dbContext = dbContext;

    public async Task<Result<List<UsersEntity>>> GetUsersAsync()
    {
        try
        {
            var users = await this.dbContext.Users
                .Include(u => u.Email)
                .ToListAsync();

            return Result.Ok(users);
        }
        catch (Exception ex)
        {
            return Result.Fail<List<UsersEntity>>(ex.Message);
        }
    }

    public async Task<Result<UsersEntity>> GetUserByIdAsync(int id)
    {
        try
        {
            var user = await this.dbContext.Users
                .Include(u => u.Email)
                .FirstOrDefaultAsync(u => u.Id == id);

            if (user is null)
            {
                return Result.Fail<UsersEntity>("User not found");
            }

            return Result.Ok(user);
        }
        catch (Exception ex)
        {
            return Result.Fail<UsersEntity>(ex.Message);
        }
    }

    public async Task<Result<UsersEntity>> CreateUserAsync(UsersEntity user)
    {
        try
        {
            this.dbContext.Users.Add(user);
            await this.dbContext.SaveChangesAsync();

            return Result.Ok(user);
        }
        catch (Exception ex)
        {
            return Result.Fail<UsersEntity>(ex.Message);
        }
    }

    public async Task<Result<UsersEntity>> UpdateUserAsync(UsersEntity user)
    {
        try
        {
            var existingUser = await this.dbContext.Users.FindAsync(user.Id);
            if (existingUser is null)
            {
                return Result.Fail<UsersEntity>($"User with id: {user.Id} not found");
            }

            existingUser.Name = user.Name;
            existingUser.Email = user.Email;
            existingUser.Password = user.Password;
            existingUser.UpdatedAt = user.UpdatedAt;

            await this.dbContext.SaveChangesAsync();
            return Result.Ok(user);
        }
        catch (Exception ex)
        {
            return Result.Fail<UsersEntity>($"Failed to update user: {ex.Message}");
        }
    }

    public async Task<Result> DeleteUserAsync(int id)
    {
        var user = await this.dbContext.Users.FindAsync(id);
        if (user is null)
        {
            return Result.Fail<UsersEntity>($"User with id: {id} not found");
        }

        this.dbContext.Users.Remove(user);
        await this.dbContext.SaveChangesAsync();

        return Result.Ok();
    }
}
