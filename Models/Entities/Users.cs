namespace Project.Models.Entities;


using System.ComponentModel.DataAnnotations.Schema;

[Table("users")]
public class UsersEntity : BaseTimestamps
{
    [Column("id")]
    public int Id { get; set; }

    [Column("name")]
    public string Name { get; set; } = string.Empty;

    [Column("email")]
    public string Email { get; set; } = string.Empty;

    [Column("password")]
    public string Password { get; set; } = string.Empty;

    public ICollection<RecipesEntity> CreatedRecipes { get; set; } = [];
    public ICollection<SavedRecipesEntity> SavedRecipes { get; set; } = [];
}
