using FilmCatalog.API.Context;
using FilmCatalog.API.Models.DTOs;
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
app.MapGet("/Actor/{actorId:int}", async Task<Results<Ok<ActorViewDTO>, NotFound<string>>> (FilmCatalogContext context, int actorId) => EntityToDTOMappers.MapActor(await context.Actors.Include(a => a.Films).SingleOrDefaultAsync(a => a.ActorId == actorId)) is ActorViewDTO actor && actor.ActorId > 0
        ? TypedResults.Ok(actor)
        : TypedResults.NotFound("Actor not found."));

app.MapGet("/Actor", Results<Ok<IEnumerable<ActorViewDTO>>, NotFound<string>> (FilmCatalogContext context) => EntityToDTOMappers.MapActors(context.Actors.Include(a => a.Films)) is IEnumerable<ActorViewDTO> actors
        ? TypedResults.Ok(actors)
        : TypedResults.NotFound("Actors not found."));

app.MapPost("Actor/Create", (FilmCatalogContext context, ActorCreateDTO createActor) =>
{

});

app.MapPut("Actor/Update/{actorId:int}", (FilmCatalogContext context, ActorUpdateDTO updateActor, int actorId) =>
{

});

app.MapDelete("Actor/Delete/{actorId:int}", (FilmCatalogContext context, int actorId) =>
{

});
#endregion

#region category endpoints
app.MapGet("/Category/{categoryId:int}", async Task<Results<Ok<CategoryViewDTO>, NotFound<string>>> (FilmCatalogContext context, int categoryId) => EntityToDTOMappers.MapCategory(await context.Categories.Include(c => c.Films).SingleOrDefaultAsync(c => c.CategoryId == categoryId)) is CategoryViewDTO category && category.CategoryId > 0
        ? TypedResults.Ok(category)
        : TypedResults.NotFound("Category not found."));

app.MapGet("/Category", Results<Ok<IEnumerable<CategoryViewDTO>>, NotFound<string>> (FilmCatalogContext context) => EntityToDTOMappers.MapCategories(context.Categories.Include(c => c.Films)) is IEnumerable<CategoryViewDTO> categories
        ? TypedResults.Ok(categories)
        : TypedResults.NotFound("Categories not found."));

app.MapPost("Category/Create", (FilmCatalogContext context, CategoryCreateDTO createCategory) =>
{

});

app.MapPut("Category/Update/{categoryId:int}", (FilmCatalogContext context, CategoryUpdateDTO updateCategory, int categoryId) =>
{

});

app.MapDelete("Category/Delete/{categoryId:int}", (FilmCatalogContext context, int categoryId) =>
{

});
#endregion

#region director endpoints
app.MapGet("/Director/{directorId:int}", async Task<Results<Ok<DirectorViewDTO>, NotFound<string>>> (FilmCatalogContext context, int directorId) => EntityToDTOMappers.MapDirector(await context.Directors.Include(d => d.Films).SingleOrDefaultAsync(d => d.DirectorId == directorId)) is DirectorViewDTO director && director.DirectorId > 0
        ? TypedResults.Ok(director)
        : TypedResults.NotFound("Director not found."));

app.MapGet("/Director", Results<Ok<IEnumerable<DirectorViewDTO>>, NotFound<string>> (FilmCatalogContext context) => EntityToDTOMappers.MapDirectors(context.Directors.Include(d => d.Films)) is IEnumerable<DirectorViewDTO> directors
        ? TypedResults.Ok(directors)
        : TypedResults.NotFound("Directors not found."));

app.MapPost("Director/Create", (FilmCatalogContext context, DirectorCreateDTO createDirector) =>
{

});

app.MapPut("Director/Update/{directorId:int}", (FilmCatalogContext context, DirectorUpdateDTO updateDirector, int directorId) =>
{

});

app.MapDelete("Director/Delete/{directorId:int}", (FilmCatalogContext context, int directorId) =>
{

});
#endregion

#region film endpoints
app.MapGet("/Film/{filmId:int}", async Task<Results<Ok<FilmViewDTO>, NotFound<string>>> (FilmCatalogContext context, int filmId) => EntityToDTOMappers.MapFilm(await context.Films.Include(f => f.Director).Include(f => f.Format).Include(f => f.Categories).Include(f => f.Actors).SingleOrDefaultAsync(f => f.FilmId == filmId)) is FilmViewDTO film && film.FilmId > 0
        ? TypedResults.Ok(film)
        : TypedResults.NotFound("Film not found."));

app.MapGet("/Film", Results<Ok<IEnumerable<FilmViewDTO>>, NotFound<string>> (FilmCatalogContext context) => EntityToDTOMappers.MapFilms(context.Films.Include(f => f.Director).Include(f => f.Format).Include(f => f.Categories).Include(f => f.Actors)) is IEnumerable<FilmViewDTO> films
        ? TypedResults.Ok(films)
        : TypedResults.NotFound("Films not found."));

app.MapPost("Film/Create", (FilmCatalogContext context, FilmCreateDTO createFilm) =>
{

});

app.MapPut("Film/Update/{filmId:int}", (FilmCatalogContext context, FilmUpdateDTO updateFilm, int filmId) =>
{

});

app.MapDelete("Film/Delete/{filmId:int}", (FilmCatalogContext context, int filmId) =>
{

});
#endregion

#region format endpoints
app.MapGet("/Format/{formatId:int}", async Task<Results<Ok<FormatViewDTO>, NotFound<string>>> (FilmCatalogContext context, int formatId) => EntityToDTOMappers.MapFormat(await context.Formats.Include(f => f.Films).SingleOrDefaultAsync(f => f.FormatId == formatId)) is FormatViewDTO format && format.FormatId > 0
        ? TypedResults.Ok(format)
        : TypedResults.NotFound("Format not found."));

app.MapGet("/Format", Results<Ok<IEnumerable<FormatViewDTO>>, NotFound<string>> (FilmCatalogContext context) => EntityToDTOMappers.MapFormats(context.Formats.Include(f => f.Films)) is IEnumerable<FormatViewDTO> formats
        ? TypedResults.Ok(formats)
        : TypedResults.NotFound("Formats not found."));

app.MapPost("Format/Create", (FilmCatalogContext context, FormatCreateDTO createFormat) =>
{

});

app.MapPut("Format/Update/{formatId:int}", (FilmCatalogContext context, FormatUpdateDTO updateFormat, int formatId) =>
{

});

app.MapDelete("Format/Delete/{formatId:int}", (FilmCatalogContext context, int formatId) =>
{

});
#endregion

app.Run();
