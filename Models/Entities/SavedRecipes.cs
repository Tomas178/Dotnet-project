using System.ComponentModel.DataAnnotations.Schema;

namespace Project.Models.Entities
{
    public class SavedRecipesEntity
    {

        [Column("recipe_id")]
        public int RecipeId { get; set; }

        [Column("user_id")]
        public int UserId { get; set; }

        [Column("created_at")]
        public DateTime CreatedAt { get; set; }
    }
}