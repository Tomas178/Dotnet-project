namespace Project.Models.Dtos.Recipes;

using Project.Models.Dtos.Users;
using Project.Models.Entities;

public class RecipesResponseDto
{
    public int Id { get; set; }
    public UsersResponseDto User { get; set; } = null!;
    public string Title { get; set; } = string.Empty;
    public string Steps { get; set; } = string.Empty;
    public int Duration { get; set; }
    public List<IngredientsEntity> Ingredients { get; set; } = [];
    public List<ToolsEntity> Tools { get; set; } = [];
}
