using System.ComponentModel.DataAnnotations.Schema;

namespace Project.Models.Entities
{
    public class SavedRecipesEntity
    {
        public int RecipeId { get; set; }
        public int UserId { get; set; }

        [Column("created_at")]
        public DateTime CreatedAt { get; set; }
    }
}