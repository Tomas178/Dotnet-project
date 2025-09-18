using System.ComponentModel.DataAnnotations.Schema;

namespace Project.Models.Entities
{
    public class RecipesIngredientsEntity
    {
        [Column("recipe_id")]
        public int RecipeId { get; set; }

        [Column("ingredient_id")]
        public int IngredientId { get; set; }
    }
}