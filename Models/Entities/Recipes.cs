namespace Project.Models.Entities;


using System.ComponentModel.DataAnnotations.Schema;

public class RecipesEntity : BaseTimestamps
{
    [Column("id")]
    public int Id { get; set; }

    [Column("user_id")]
    public int UserId { get; set; }
    public UsersEntity User { get; set; } = null!;

    [Column("title")]
    public string Title { get; set; } = string.Empty;

    [Column("steps")]
    public string Steps { get; set; } = string.Empty;

    [Column("duration")]
    public int Duration { get; set; }
}
