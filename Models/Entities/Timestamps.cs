namespace Project.Models.Entities;

using System.ComponentModel.DataAnnotations.Schema;

public abstract class BaseTimestamps
{
    [Column("created_at")]
    public DateTime CreatedAt { get; set; }

    [Column("updated_at")]
    public DateTime UpdatedAt { get; set; }
}

public abstract class BaseCreatedTimestamp
{
    [Column("created_at")]
    public DateTime CreatedAt { get; set; }
}
