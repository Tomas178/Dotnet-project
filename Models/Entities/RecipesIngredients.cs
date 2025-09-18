namespace Project.Models.Entities;

using System.ComponentModel.DataAnnotations.Schema;

public class RecipesIngredientsEntity
{
    [Column("recipe_id")]
    public int RecipeId { get; set; }

    [Column("ingredient_id")]
    public int IngredientId { get; set; }
}
