using System.ComponentModel.DataAnnotations.Schema;

namespace Project.Models.Entities
{
    public class ToolsEntity
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("name")]
        public string Name { get; set; } = string.Empty;

        [Column("created_at")]
        public DateTime CreatedAt { get; set; }
    }
}