using Project.Database;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration["ConnectionStrings:DefaultConnection"];

builder.Services.AddDbContext<ProjectDbContext>(options =>
    options.UseNpgsql(connectionString));

var app = builder.Build();


app.UseHttpsRedirection();

app.Run();
