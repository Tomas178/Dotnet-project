namespace Project.Database;

using Microsoft.EntityFrameworkCore;
using Project.Models.Entities;

public class ProjectDbContext(DbContextOptions<ProjectDbContext> options) : DbContext(options)
{
    public DbSet<UsersEntity> Users { get; set; }
    public DbSet<RecipesEntity> Recipes { get; set; }
    public DbSet<SavedRecipesEntity> SavedRecipes { get; set; }
    public DbSet<ToolsEntity> Tools { get; set; }
    public DbSet<RecipesToolsEntity> RecipesTools { get; set; }
    public DbSet<IngredientsEntity> Ingredients { get; set; }
    public DbSet<RecipesIngredientsEntity> RecipesIngredients { get; set; }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        this.AddTimestamps();
        return base.SaveChangesAsync(cancellationToken);
    }

    private void AddTimestamps()
    {
        var now = DateTime.UtcNow;

        var timestampedEntities = this.ChangeTracker.Entries()
            .Where(et => et.Entity is BaseTimestamps &&
                (et.State == EntityState.Added || et.State == EntityState.Modified));

        foreach (var entry in timestampedEntities)
        {
            var entity = (BaseTimestamps)entry.Entity;

            if (entry.State == EntityState.Added)
            {
                entity.CreatedAt = now;
            }

            entity.UpdatedAt = now;
        }

        var createdOnlyEntities = this.ChangeTracker.Entries()
            .Where(x => x.Entity is BaseCreatedTimestamp && x.State == EntityState.Added);

        foreach (var entry in createdOnlyEntities)
        {
            ((BaseCreatedTimestamp)entry.Entity).CreatedAt = now;
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<RecipesEntity>()
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

