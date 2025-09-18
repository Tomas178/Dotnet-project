#nullable disable

namespace Project.Migrations;

using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

/// <inheritdoc />
public partial class InitialCreate : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "Ingredients",
            columns: table => new
            {
                id = table.Column<int>(type: "integer", nullable: false)
                    .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                name = table.Column<string>(type: "text", nullable: false),
                created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
            },
            constraints: table => table.PrimaryKey("PK_Ingredients", x => x.id));

        migrationBuilder.CreateTable(
            name: "RecipesIngredients",
            columns: table => new
            {
                recipe_id = table.Column<int>(type: "integer", nullable: false),
                ingredient_id = table.Column<int>(type: "integer", nullable: false)
            },
            constraints: table => table.PrimaryKey("PK_RecipesIngredients", x => new { x.recipe_id, x.ingredient_id }));

        migrationBuilder.CreateTable(
            name: "RecipesTools",
            columns: table => new
            {
                recipe_id = table.Column<int>(type: "integer", nullable: false),
                tool_id = table.Column<int>(type: "integer", nullable: false)
            },
            constraints: table => table.PrimaryKey("PK_RecipesTools", x => new { x.recipe_id, x.tool_id }));

        migrationBuilder.CreateTable(
            name: "Tools",
            columns: table => new
            {
                id = table.Column<int>(type: "integer", nullable: false)
                    .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                name = table.Column<string>(type: "text", nullable: false),
                created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
            },
            constraints: table => table.PrimaryKey("PK_Tools", x => x.id));

        migrationBuilder.CreateTable(
            name: "Users",
            columns: table => new
            {
                id = table.Column<int>(type: "integer", nullable: false)
                    .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                name = table.Column<string>(type: "text", nullable: false),
                email = table.Column<string>(type: "text", nullable: false),
                created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
            },
            constraints: table => table.PrimaryKey("PK_Users", x => x.id));

        migrationBuilder.CreateTable(
            name: "Recipes",
            columns: table => new
            {
                id = table.Column<int>(type: "integer", nullable: false)
                    .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                user_id = table.Column<int>(type: "integer", nullable: false),
                title = table.Column<string>(type: "text", nullable: false),
                steps = table.Column<string>(type: "text", nullable: false),
                duration = table.Column<int>(type: "integer", nullable: false),
                created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Recipes", x => x.id);
                table.ForeignKey(
                    name: "FK_Recipes_Users_user_id",
                    column: x => x.user_id,
                    principalTable: "Users",
                    principalColumn: "id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "SavedRecipes",
            columns: table => new
            {
                recipe_id = table.Column<int>(type: "integer", nullable: false),
                user_id = table.Column<int>(type: "integer", nullable: false),
                created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_SavedRecipes", x => new { x.user_id, x.recipe_id });
                table.ForeignKey(
                    name: "FK_SavedRecipes_Recipes_recipe_id",
                    column: x => x.recipe_id,
                    principalTable: "Recipes",
                    principalColumn: "id",
                    onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    name: "FK_SavedRecipes_Users_user_id",
                    column: x => x.user_id,
                    principalTable: "Users",
                    principalColumn: "id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateIndex(
            name: "IX_Recipes_user_id",
            table: "Recipes",
            column: "user_id");

        migrationBuilder.CreateIndex(
            name: "IX_SavedRecipes_recipe_id",
            table: "SavedRecipes",
            column: "recipe_id");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "Ingredients");

        migrationBuilder.DropTable(
            name: "RecipesIngredients");

        migrationBuilder.DropTable(
            name: "RecipesTools");

        migrationBuilder.DropTable(
            name: "SavedRecipes");

        migrationBuilder.DropTable(
            name: "Tools");

        migrationBuilder.DropTable(
            name: "Recipes");

        migrationBuilder.DropTable(
            name: "Users");
    }
}
