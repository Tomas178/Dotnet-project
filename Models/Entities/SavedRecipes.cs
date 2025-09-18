namespace Project.Models.Entities;


using System.ComponentModel.DataAnnotations.Schema;

public class SavedRecipesEntity
{

    [Column("recipe_id")]
    public int RecipeId { get; set; }

    [Column("user_id")]
    public int UserId { get; set; }

    [Column("created_at")]
    public DateTime CreatedAt { get; set; }
}
