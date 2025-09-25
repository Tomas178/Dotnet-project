namespace Project.Utils;

using Project.Models.Dtos.Recipes;
using Project.Models.Dtos.SavedRecipes;
using Project.Models.Dtos.Users;
using Project.Models.Entities;

public static class Mapper
{
    public static RecipesResponseDto MapToResponseDto(RecipesEntity recipe)
    {
        return new RecipesResponseDto
        {
            Id = recipe.Id,
            User = MapToResponseDto(recipe.User),
            Title = recipe.Title,
            Steps = [.. recipe.Steps.Split('\n')],
            Duration = recipe.Duration,
            Ingredients = [.. recipe.RecipesIngredients.Select(ri => ri.Ingredient.Name)],
            Tools = [.. recipe.RecipesTools.Select(rt => rt.Tool.Name)],
        };
    }

    public static ICollection<RecipesResponseDto> MapToResponseDto(IEnumerable<RecipesEntity> recipes)
    {
        return [.. recipes.Select(MapToResponseDto)];
    }


    public static UsersResponseDto MapToResponseDto(UsersEntity user, bool includeRecipes = false)
    {
        return new UsersResponseDto
        {
            Id = user.Id,
            Name = user.Name,

            CreatedRecipes = includeRecipes
                ? MapToResponseDto(user.CreatedRecipes)
                : null,
            SavedRecipes = includeRecipes
                ? MapToResponseDto(user.SavedRecipes.Select(sr => sr.Recipe))
                : null
        };
    }

    public static ICollection<UsersResponseDto> MapToResponseDto(IEnumerable<UsersEntity> users, bool includeRecipes = false)
    {
        return [.. users.Select(user => MapToResponseDto(user, includeRecipes))];
    }

    public static SavedRecipesResponseDto MapToResponseDto(SavedRecipesEntity link)
    {
        return new SavedRecipesResponseDto
        {
            Recipe = MapToResponseDto(link.Recipe),
            User = MapToResponseDto(link.User)
        };
    }
}
