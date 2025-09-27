namespace Project.Repositories.Interfaces;

using Project.Models.Core;
using Project.Models.Entities;

public interface IToolsRepository
{
    public Task<Result<List<ToolsEntity>>> CreateToolsAsync(List<ToolsEntity> tools);
    public Task<Result<List<ToolsEntity>>> GetToolsAsync();
    public Task<Result<ToolsEntity>> GetByIdAsync(int id);
    public Task<Result<List<ToolsEntity>>> GetByNamesAsync(List<string> names);
}
