namespace Project.Models.Dtos.Recipes;

using System.ComponentModel.DataAnnotations;

public class CreateRecipesRequestDto
{
    public int UserId { get; set; }

    [Required(ErrorMessage = "Title is required")]
    public string Title { get; set; } = string.Empty;
    public int Duration { get; set; }

    [Required(ErrorMessage = "At least one step is required")]
    [MinLength(1, ErrorMessage = "At least one step is required")]
    public List<string> Steps { get; set; } = [];

    [Required(ErrorMessage = "At least one ingredient is required")]
    [MinLength(1, ErrorMessage = "At least one ingredient is required")]
    public List<string> Ingredients { get; set; } = [];
    public List<string> Tools { get; set; } = [];
}
