namespace Project.Models.Dtos.SavedRecipes;

using Project.Models.Dtos.Recipes;
using Project.Models.Dtos.Users;

public class SavedRecipesResponseDto
{
    public RecipesResponseDto? Recipe { get; set; }
    public UsersResponseDto? User { get; set; }
}
