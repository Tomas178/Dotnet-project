namespace Project.Models.Dtos.SavedRecipes;

using System.ComponentModel.DataAnnotations;

public class PostLinkRequestDto
{
    [Required(ErrorMessage = "Recipe ID is required")]
    public int RecipeId { get; set; }

    [Required(ErrorMessage = "User ID is required")]
    public int UserId { get; set; }
}
