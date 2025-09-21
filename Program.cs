using Project.Database;
using Project.Repositories;
using Project.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Project.Services.Interfaces;
using Project.Services;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration["ConnectionStrings:DefaultConnection"];

builder.Services.AddScoped<IUsersRepository, UsersRepository>();
builder.Services.AddScoped<IRecipesRepository, RecipesRepository>();
builder.Services.AddScoped<ISavedRecipesRepository, SavedRecipesRepository>();
builder.Services.AddScoped<IToolsRepository, ToolsRepository>();
builder.Services.AddScoped<IRecipesToolsRepository, RecipesToolsRepository>();
builder.Services.AddScoped<IIngredientsRepository, IngredientsRepository>();
builder.Services.AddScoped<IRecipesIngredientsRepository, RecipesIngredientsRepository>();

builder.Services.AddScoped<IUsersService, UsersService>();
builder.Services.AddScoped<IRecipesService, RecipesService>();
builder.Services.AddScoped<ISavedRecipesService, SavedRecipesService>();

builder.Services.AddDbContext<ProjectDbContext>(options =>
    options.UseNpgsql(connectionString));

var app = builder.Build();


app.UseHttpsRedirection();

app.Run();
