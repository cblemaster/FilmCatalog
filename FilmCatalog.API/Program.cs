using FilmCatalog.API.Context;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

IConfigurationRoot config = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true)
            .Build();

string connectionString = config.GetConnectionString("Project") ?? "Error retrieving connection string!";

builder.Services.AddDbContext<FilmCatalogContext>(options => options.UseSqlServer(connectionString));

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

#region actor endpoints
app.MapGet("/actor/{actorId:int}", () => { });
app.MapGet("/actor", () => { });
app.MapPost("/actor", () => { });
app.MapPut("/actor/{actorId:int}", () => { });
app.MapDelete("/actor/{actorId:int}", () => { });
#endregion

#region category endpoints
app.MapGet("/category/{categoryId:int}", () => { });
app.MapGet("/category", () => { });
app.MapPost("/category", () => { });
app.MapDelete("/category/{categoryId:int}", () => { });
#endregion

#region director endpoints
app.MapGet("/director/{directorId:int}", () => { });
app.MapGet("/director", () => { });
app.MapPost("/director", () => { });
app.MapPut("/director/{directorId:int}", () => { });
app.MapDelete("/director/{directorId:int}", () => { });
#endregion

#region film endpoints
app.MapGet("/film/{filmId:int}", () => { });
app.MapGet("/film", () => { });
app.MapGet("/film/favorite", () => { });
app.MapGet("/film/rare", () => { });
app.MapGet("/film/fivestar", () => { });
app.MapPost("/film", () => { });
app.MapPut("/film/{filmId:int}", () => { });
app.MapPut("/film/{filmId:int}/actor", () => { });
app.MapPut("/film/{filmId:int}/categoty", () => { });
app.MapDelete("/film/{filmId:int}", () => { });
#endregion

#region format endpoints
app.MapGet("/format/{formatId:int}", () => { });
app.MapGet("/format", () => { });
app.MapPost("/format", () => { });
app.MapDelete("/format/{formatId:int}", () => { });
#endregion

app.Run();
