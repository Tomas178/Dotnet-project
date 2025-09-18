namespace Project.Models.Entities;


using System.ComponentModel.DataAnnotations.Schema;

public class RecipesToolsEntity
{
    [Column("recipe_id")]
    public int RecipeId { get; set; }

    [Column("tool_id")]
    public int ToolId { get; set; }
}
