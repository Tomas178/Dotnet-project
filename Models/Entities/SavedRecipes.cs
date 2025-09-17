namespace Project.Models.Entities
{
    public class SavedRecipesEntity
    {
        public int RecipeId { get; set; }
        public int UserId { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}