namespace Project.Models.Entities;


using System.ComponentModel.DataAnnotations.Schema;

[Table("recipes_tools")]
public class RecipesToolsEntity : BaseTimestamps
{
    [Column("recipe_id")]
    public int RecipeId { get; set; }

    [Column("tool_id")]
    public int ToolId { get; set; }
}
