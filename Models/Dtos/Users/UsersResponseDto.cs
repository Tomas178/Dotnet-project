namespace Project.Models.Dtos.Users;

using Project.Models.Entities;

public class UsersResponseDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public ICollection<RecipesEntity> Recipes { get; set; } = [];
}
