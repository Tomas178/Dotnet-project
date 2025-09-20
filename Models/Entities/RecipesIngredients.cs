namespace Project.Models.Entities;

using System.ComponentModel.DataAnnotations.Schema;

[Table("recipes_ingredients")]
public class RecipesIngredientsEntity : BaseTimestamps
{
    [Column("recipe_id")]
    public int RecipeId { get; set; }

    [Column("ingredient_id")]
    public int IngredientId { get; set; }
}
