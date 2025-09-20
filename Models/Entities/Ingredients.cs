namespace Project.Models.Entities;


using System.ComponentModel.DataAnnotations.Schema;

[Table("ingredients")]
public class IngredientsEntity : BaseCreatedTimestamp
{
    [Column("id")]
    public int Id { get; set; }

    [Column("name")]
    public string Name { get; set; } = string.Empty;
}
