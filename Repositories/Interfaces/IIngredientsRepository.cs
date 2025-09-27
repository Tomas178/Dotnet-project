namespace Project.Repositories.Interfaces;

using Project.Models.Core;
using Project.Models.Entities;

public interface IIngredientsRepository
{
    public Task<Result<List<IngredientsEntity>>> CreateIngredientsAsync(List<IngredientsEntity> ingredients);
    public Task<Result<List<IngredientsEntity>>> GetIngredientsAsync();
    public Task<Result<IngredientsEntity>> GetByIdAsync(int id);
    public Task<Result<List<IngredientsEntity>>> GetByNamesAsync(List<string> names);
}
