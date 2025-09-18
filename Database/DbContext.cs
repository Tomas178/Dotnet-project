namespace Project.Database;

using Microsoft.EntityFrameworkCore;
using Project.Models.Entities;

public class ProjectDbContext(DbContextOptions<ProjectDbContext> options) : DbContext(options)
{
    public DbSet<UsersEntity> Users { get; set; }
    public DbSet<RecipeEntity> Recipes { get; set; }
    public DbSet<SavedRecipesEntity> SavedRecipes { get; set; }
    public DbSet<ToolsEntity> Tools { get; set; }
    public DbSet<RecipesToolsEntity> RecipesTools { get; set; }
    public DbSet<IngredientsEntity> Ingredients { get; set; }
    public DbSet<RecipesIngredientsEntity> RecipesIngredients { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<RecipeEntity>()
            .HasOne(r => r.User)
            .WithMany(u => u.Recipes)
            .HasForeignKey(r => r.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<SavedRecipesEntity>()
            .HasKey(sr => new { sr.UserId, sr.RecipeId });

        modelBuilder.Entity<RecipesToolsEntity>()
            .HasKey(rt => new { rt.RecipeId, rt.ToolId });

        modelBuilder.Entity<RecipesIngredientsEntity>()
            .HasKey(ri => new { ri.RecipeId, ri.IngredientId });
    }
}

