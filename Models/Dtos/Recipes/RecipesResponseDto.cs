namespace Project.Models.Dtos.Recipes;

using Project.Models.Dtos.Users;

public class RecipesResponseDto
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public UsersResponseDto User { get; set; } = null!;
    public string Title { get; set; } = string.Empty;
    public List<string> Steps { get; set; } = [];
    public int Duration { get; set; }
    public List<string> Ingredients { get; set; } = [];
    public List<string> Tools { get; set; } = [];
}
