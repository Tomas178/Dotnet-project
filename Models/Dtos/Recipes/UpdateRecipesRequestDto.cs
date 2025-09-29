namespace Project.Models.Dtos.Recipes;

public class UpdateRecipesRequestDto
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public string Title { get; set; } = string.Empty;
    public List<string> Steps { get; set; } = [];
    public int Duration { get; set; }
    public List<string> Ingredients { get; set; } = [];
    public List<string> Tools { get; set; } = [];
}
