using System.ComponentModel.DataAnnotations.Schema;

namespace Project.Models.Entities
{
    public class RecipeEntity
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("user_id")]
        public int UserId { get; set; }

        [Column("title")]
        public string Title { get; set; } = string.Empty;

        [Column("steps")]
        public List<string> Steps { get; set; } = [];

        [Column("duration")]
        public int Duration { get; set; }

        [Column("created_at")]
        public DateTime CreatedAt { get; set; }

        [Column("updated_at")]
        public DateTime UpdatedAt { get; set; }
    }
}