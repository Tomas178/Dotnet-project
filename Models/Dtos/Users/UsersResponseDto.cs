namespace Project.Models.Dtos.Users;

using Project.Models.Dtos.Recipes;

public class UsersResponseDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public ICollection<RecipesResponseDto>? CreatedRecipes { get; set; }
    public ICollection<RecipesResponseDto>? SavedRecipes { get; set; }
}
