using System.ComponentModel.DataAnnotations.Schema;

namespace Project.Models.Entities
{
    public class IngredientsEntity
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        [Column("created_at")]
        public DateTime CreatedAt { get; set; }
    }
}