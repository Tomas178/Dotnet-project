namespace Project.Repositories;

using Microsoft.EntityFrameworkCore;
using Project.Database;
using Project.Models.Core;
using Project.Models.Entities;
using Project.Repositories.Interfaces;

public class ToolsRepository(ProjectDbContext dbContext) : IToolsRepository
{
    private readonly ProjectDbContext dbContext = dbContext;

    public async Task<Result<List<ToolsEntity>>> CreateToolsAsync(List<ToolsEntity> tools)
    {
        try
        {
            this.dbContext.Tools.AddRange(tools);
            await this.dbContext.SaveChangesAsync();

            return Result.Ok(tools);
        }
        catch (Exception ex)
        {
            return Result.Fail<List<ToolsEntity>>(ex.Message);
        }
    }

    public async Task<Result<ToolsEntity>> GetByIdAsync(int id)
    {
        try
        {
            var tool = await this.dbContext.Tools.FindAsync(id);
            if (tool is null)
            {
                return Result.Fail<ToolsEntity>($"Tool with id: {id} not found");
            }

            return Result.Ok(tool);
        }
        catch (Exception ex)
        {
            return Result.Fail<ToolsEntity>(ex.Message);
        }
    }

    public async Task<Result<List<ToolsEntity>>> GetByNamesAsync(List<string> names)
    {
        try
        {
            var tools = await this.dbContext.Tools.Where(t => names.Contains(t.Name)).ToListAsync();
            if (tools is null)
            {
                return Result.Fail<List<ToolsEntity>>("Recipes with given names have not been found");
            }

            return Result.Ok(tools);
        }
        catch (Exception ex)
        {
            return Result.Fail<List<ToolsEntity>>(ex.Message);
        }
    }
}
