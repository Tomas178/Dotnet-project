namespace Project.Models.Entities;


using System.ComponentModel.DataAnnotations.Schema;

public class SavedRecipesEntity : BaseCreatedTimestamp
{

    [Column("recipe_id")]
    public int RecipeId { get; set; }
    public RecipesEntity Recipe { get; set; } = null!;

    [Column("user_id")]
    public int UserId { get; set; }
    public UsersEntity User { get; set; } = null!;
}
