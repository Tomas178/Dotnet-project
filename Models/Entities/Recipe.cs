using System.ComponentModel.DataAnnotations.Schema;

namespace Project.Models.Entities
{
    public class RecipeEntity
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Title { get; set; } = string.Empty;
        public List<string> Steps { get; set; } = [];
        public int Duration { get; set; }

        [Column("created_at")]
        public DateTime CreatedAt { get; set; }

        [Column("updated_at")]
        public DateTime UpdatedAt { get; set; }
    }
}