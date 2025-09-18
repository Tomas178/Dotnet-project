namespace Project.Models.Entities;


using System.ComponentModel.DataAnnotations.Schema;

public class IngredientsEntity
{
    [Column("id")]
    public int Id { get; set; }

    [Column("name")]
    public string Name { get; set; } = string.Empty;

    [Column("created_at")]
    public DateTime CreatedAt { get; set; }
}
