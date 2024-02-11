using FilmCatalog.API.Context;
using FilmCatalog.API.Models.DTOs;
using FilmCatalog.API.Models.Entities;
using FilmCatalog.API.Models.Mappers;
using Microsoft.AspNetCore.Http.HttpResults;
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
app.MapGet("/actor/{actorId:int}", async Task<Results<BadRequest<string>, Ok<DisplayActor>, NotFound<string>>> (FilmCatalogContext context, int actorId) =>
{
    if (actorId < 1)
    {
        return TypedResults.BadRequest("Invalid actor id.");
    }
    if (await context.Actors.SingleOrDefaultAsync(a => a.ActorId == actorId) is Actor actor) 
    {
        DisplayActor displayActor = EntityToDTOMappers.MapActor(actor);
        return TypedResults.Ok(displayActor);
    }
    return TypedResults.NotFound($"No actor with actor id {actorId} found.");
});

app.MapGet("/actor", async Task<Results<Ok<IEnumerable<DisplayActor>>, NotFound<string>>> (FilmCatalogContext context) =>
{
    if (context.Actors is IEnumerable<Actor> actors && actors.Count() > 0)
    {
        IEnumerable<DisplayActor> displayActors = EntityToDTOMappers.MapActorCollection(actors);
        return TypedResults.Ok(displayActors);
    }
    return TypedResults.NotFound("No actors found.");
});

app.MapPost("/actor", () => { });
app.MapPut("/actor/{actorId:int}", () => { });
app.MapDelete("/actor/{actorId:int}", () => { });
#endregion

#region category endpoints
app.MapGet("/category/{categoryId:int}", async Task<Results<BadRequest<string>, Ok<DisplayCategory>, NotFound<string>>> (FilmCatalogContext context, int categoryId) =>
{
    if (categoryId < 1)
    {
        return TypedResults.BadRequest("Invalid category id.");
    }
    if (await context.Categories.SingleOrDefaultAsync(c => c.CategoryId == categoryId) is Category category)
    {
        DisplayCategory displayCategory = EntityToDTOMappers.MapCategory(category);
        return TypedResults.Ok(displayCategory);
    }
    return TypedResults.NotFound($"No category with category id {categoryId} found.");
});

app.MapGet("/category",async Task<Results<Ok<IEnumerable<DisplayCategory>>, NotFound<string>>> (FilmCatalogContext context) =>
{
    if (context.Categories is IEnumerable<Category> categories && categories.Count() > 0)
    {
        IEnumerable<DisplayCategory> displayCategories = EntityToDTOMappers.MapCategoryCollection(categories);
        return TypedResults.Ok(displayCategories);
    }
    return TypedResults.NotFound("No categories found.");
});

app.MapPost("/category", () => { });
app.MapDelete("/category/{categoryId:int}", () => { });
#endregion

#region director endpoints
app.MapGet("/director/{directorId:int}", async Task<Results<BadRequest<string>, Ok<DisplayDirector>, NotFound<string>>> (FilmCatalogContext context, int directorId) =>
{
    if (directorId < 1)
    {
        return TypedResults.BadRequest("Invalid director id.");
    }
    if (await context.Directors.SingleOrDefaultAsync(d => d.DirectorId == directorId) is Director director)
    {
        DisplayDirector displayDirector = EntityToDTOMappers.MapDirector(director);
        return TypedResults.Ok(displayDirector);
    }
    return TypedResults.NotFound($"No director with director id {directorId} found.");
});

app.MapGet("/director", async Task<Results<Ok<IEnumerable<DisplayDirector>>, NotFound<string>>> (FilmCatalogContext context) =>
{
    if (context.Directors is IEnumerable<Director> directors && directors.Count() > 0)
    {
        IEnumerable<DisplayDirector> displayDirectors = EntityToDTOMappers.MapDirectorCollection(directors);
        return TypedResults.Ok(displayDirectors);
    }
    return TypedResults.NotFound("No directors found.");
});

app.MapPost("/director", () => { });
app.MapPut("/director/{directorId:int}", () => { });
app.MapDelete("/director/{directorId:int}", () => { });
#endregion

#region film endpoints
app.MapGet("/film/{filmId:int}", async Task<Results<BadRequest<string>, Ok<DisplayFilm>, NotFound<string>>> (FilmCatalogContext context, int filmId) =>
{
    if (filmId < 1)
    {
        return TypedResults.BadRequest("Invalid film id.");
    }
    if (await context.Films.SingleOrDefaultAsync(f => f.FilmId == filmId) is Film film)
    {
        DisplayFilm displayFilm = EntityToDTOMappers.MapFilm(film);
        return TypedResults.Ok(displayFilm);
    }
    return TypedResults.NotFound($"No film with film id {filmId} found.");
});

app.MapGet("/film", Results<Ok<IEnumerable<DisplayFilm>>, NotFound<string>> (FilmCatalogContext context) =>
{
    if (context.Films is IEnumerable<Film> films && films.Count() > 0)
    {
        IEnumerable<DisplayFilm> displayFilms = EntityToDTOMappers.MapFilmCollection(films);
        return TypedResults.Ok(displayFilms);
    }
    return TypedResults.NotFound("No films found.");
});

app.MapGet("/film/favorite", Results<Ok<IEnumerable<DisplayFilm>>, NotFound<string>> (FilmCatalogContext context) =>
{
    if (context.Films.Where(f => f.IsFavorite) is IEnumerable<Film> films && films.Count() > 0)
    {
        IEnumerable<DisplayFilm> displayFilms = EntityToDTOMappers.MapFilmCollection(films);
        return TypedResults.Ok(displayFilms);
    }
    return TypedResults.NotFound("No favorite films found.");
});

app.MapGet("/film/rare", Results<Ok<IEnumerable<DisplayFilm>>, NotFound<string>> (FilmCatalogContext context) =>
{
    if (context.Films.Where(f => f.IsRareCollectibleAndOrValuable) is IEnumerable<Film> films && films.Count() > 0)
    {
        IEnumerable<DisplayFilm> displayFilms = EntityToDTOMappers.MapFilmCollection(films);
        return TypedResults.Ok(displayFilms);
    }
    return TypedResults.NotFound("No rare, collectible, nor valuable films found.");
});

app.MapGet("/film/fivestar", Results<Ok<IEnumerable<DisplayFilm>>, NotFound<string>> (FilmCatalogContext context) =>
{
    if (context.Films.Where(f => f.StarRating == 5) is IEnumerable<Film> films && films.Count() > 0)
    {
        IEnumerable<DisplayFilm> displayFilms = EntityToDTOMappers.MapFilmCollection(films);
        return TypedResults.Ok(displayFilms);
    }
    return TypedResults.NotFound("No rare, collectible, nor valuable films found.");
});

app.MapPost("/film", () => { });
app.MapPut("/film/{filmId:int}", () => { });
app.MapPut("/film/{filmId:int}/actor", () => { });
app.MapPut("/film/{filmId:int}/categoty", () => { });
app.MapDelete("/film/{filmId:int}", () => { });
#endregion

#region format endpoints
app.MapGet("/format/{formatId:int}", async Task<Results<BadRequest<string>, Ok<DisplayFormat>, NotFound<string>>> (FilmCatalogContext context, int formatId) =>
{
    if (formatId < 1)
    {
        return TypedResults.BadRequest("Invalid format id.");
    }
    if (await context.Formats.SingleOrDefaultAsync(f => f.FormatId == formatId) is Format format)
    {
        DisplayFormat displayFormat = EntityToDTOMappers.MapFormat(format);
        return TypedResults.Ok(displayFormat);
    }
    return TypedResults.NotFound($"No format with format id {formatId} found.");
});

app.MapGet("/format", async Task<Results<Ok<IEnumerable<DisplayFormat>>, NotFound<string>>> (FilmCatalogContext context) =>
{
    if (context.Formats is IEnumerable<Format> formats && formats.Count() > 0)
    {
        IEnumerable<DisplayFormat> displayFormats = EntityToDTOMappers.MapFormatCollection(formats);
        return TypedResults.Ok(displayFormats);
    }
    return TypedResults.NotFound("No formats found.");
});

app.MapPost("/format", () => { });
app.MapDelete("/format/{formatId:int}", () => { });
#endregion

app.Run();
