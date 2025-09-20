namespace Project.Repositories.Interfaces;

using Project.Models.Core;
using Project.Models.Entities;

public interface IRecipesToolsRepository
{
    public Task<Result<List<RecipesToolsEntity>>> CreatedRecipesToolsLinkAsync(List<RecipesToolsEntity> links);
}
