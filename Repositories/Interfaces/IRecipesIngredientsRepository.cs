namespace Project.Repositories.Interfaces;

using Project.Models.Core;
using Project.Models.Entities;

public interface IRecipesIngredientsRepository
{
    public Task<Result<List<RecipesIngredientsEntity>>> CreatedRecipesIngredientsLinkAsync(List<RecipesIngredientsEntity> links);
}
