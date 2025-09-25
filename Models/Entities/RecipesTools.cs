namespace Project.Models.Entities;


using System.ComponentModel.DataAnnotations.Schema;

[Table("recipes_tools")]
public class RecipesToolsEntity : BaseCreatedTimestamp
{
    [Column("recipe_id")]
    public int RecipeId { get; set; }
    public RecipesEntity Recipe { get; set; } = null!;

    [Column("tool_id")]
    public int ToolId { get; set; }
    public ToolsEntity Tool { get; set; } = null!;
}
