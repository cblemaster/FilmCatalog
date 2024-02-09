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
app.MapGet("/Actor/{actorId:int}", async Task<Results<Ok<ActorViewWithFilmsDTO>, NotFound<string>>> (FilmCatalogContext context, int actorId) =>
    EntityToDTOMappers.MapActorWithFilms(await context.Actors
        .Include(a => a.Films).ThenInclude(f => f.Director)
        .Include(a => a.Films).ThenInclude(f => f.Format)
        .Include(a => a.Films).ThenInclude(f => f.Categories)
        .Include(a => a.Films).ThenInclude(f => f.Actors)
    .SingleOrDefaultAsync(a => a.ActorId == actorId))
    is ActorViewWithFilmsDTO actor && actor.ActorId > 0
        ? TypedResults.Ok(actor)
        : TypedResults.NotFound("Actor not found."));

app.MapGet("/Actor", Results<Ok<IEnumerable<ActorViewWithFilmsDTO>>, NotFound<string>> (FilmCatalogContext context) =>
    EntityToDTOMappers.MapActorsWithFilms(context.Actors
        .Include(a => a.Films).ThenInclude(f => f.Director)
        .Include(a => a.Films).ThenInclude(f => f.Format)
        .Include(a => a.Films).ThenInclude(f => f.Categories)
        .Include(a => a.Films).ThenInclude(f => f.Actors))
    is IEnumerable<ActorViewWithFilmsDTO> actors
        ? TypedResults.Ok(actors)
        : TypedResults.NotFound("Actors not found."));

app.MapPost("/Actor", async Task<Results<BadRequest<string>, Created<Actor>>> (FilmCatalogContext context, ActorCreateDTO createActor) =>
{
    (bool IsValid, string ErrorMessage) = createActor.Validate();
    if (!IsValid)
    {
        return TypedResults.BadRequest(ErrorMessage);
    }

    Actor? actorToCreate = DTOToEntityMappers.MapActorCreate(createActor);

    if (actorToCreate is not null)
    {
        context.Actors.Add(actorToCreate);
        await context.SaveChangesAsync();
        return TypedResults.Created($"/Actor/{actorToCreate.ActorId}", actorToCreate);
    }

    return TypedResults.BadRequest("Unable to create actor.");
});

app.MapPut("/Actor/{actorId:int}", (FilmCatalogContext context, ActorUpdateDTO updateActor, int actorId) =>
{

});

app.MapDelete("/Actor/{actorId:int}", async Task<Results<BadRequest<string>, NoContent, NotFound<string>>> (FilmCatalogContext context, int actorId) =>
{
    if (actorId < 1)
    {
        return TypedResults.BadRequest("Invalid actor id.");
    }

    Actor actorToDelete = await context.Actors.SingleOrDefaultAsync(a => a.ActorId == actorId);

    if (actorToDelete is not null)
    {
        context.Actors.Remove(actorToDelete);
        await context.SaveChangesAsync();
        return TypedResults.NoContent();
    }

    return TypedResults.NotFound("Actor to delete not found.");
});
#endregion

#region category endpoints
app.MapGet("/Category/{categoryId:int}", async Task<Results<Ok<CategoryViewWithFilmsDTO>, NotFound<string>>> (FilmCatalogContext context, int categoryId) =>
    EntityToDTOMappers.MapCategoryWithFilms(await context.Categories
        .Include(c => c.Films).ThenInclude(f => f.Director)
        .Include(c => c.Films).ThenInclude(f => f.Format)
        .Include(c => c.Films).ThenInclude(f => f.Categories)
        .Include(c => c.Films).ThenInclude(f => f.Actors)
    .SingleOrDefaultAsync(c => c.CategoryId == categoryId))
    is CategoryViewWithFilmsDTO category && category.CategoryId > 0
        ? TypedResults.Ok(category)
        : TypedResults.NotFound("Category not found."));

app.MapGet("/Category", Results<Ok<IEnumerable<CategoryViewWithFilmsDTO>>, NotFound<string>> (FilmCatalogContext context) =>
    EntityToDTOMappers.MapCategoriesWithFilms(context.Categories
        .Include(c => c.Films).ThenInclude(f => f.Director)
        .Include(c => c.Films).ThenInclude(f => f.Format)
        .Include(c => c.Films).ThenInclude(f => f.Categories)
        .Include(c => c.Films).ThenInclude(f => f.Actors))
    is IEnumerable<CategoryViewWithFilmsDTO> categories
        ? TypedResults.Ok(categories)
        : TypedResults.NotFound("Categories not found."));

app.MapPost("/Category", async Task<Results<BadRequest<string>, Created<Category>>> (FilmCatalogContext context, CategoryCreateDTO createCategory) =>
{
    (bool IsValid, string ErrorMessage) = createCategory.Validate();
    if (!IsValid)
    {
        return TypedResults.BadRequest(ErrorMessage);
    }

    Category? categoryToCreate = DTOToEntityMappers.MapCategoryCreate(createCategory);

    if (categoryToCreate is not null)
    {
        context.Categories.Add(categoryToCreate);
        await context.SaveChangesAsync();
        return TypedResults.Created($"/Category/{categoryToCreate.CategoryId}", categoryToCreate);
    }

    return TypedResults.BadRequest("Unable to create category.");
});

app.MapPut("/Category/{categoryId:int}", (FilmCatalogContext context, CategoryUpdateDTO updateCategory, int categoryId) =>
{

});

app.MapDelete("/Category/{categoryId:int}", async Task<Results<BadRequest<string>, NoContent, NotFound<string>>> (FilmCatalogContext context, int categoryId) =>
{
    if (categoryId < 1)
    {
        return TypedResults.BadRequest("Invalid category id.");
    }

    Category categoryToDelete = await context.Categories.SingleOrDefaultAsync(c => c.CategoryId == categoryId);

    if (categoryToDelete is not null)
    {
        context.Categories.Remove(categoryToDelete);
        await context.SaveChangesAsync();
        return TypedResults.NoContent();
    }

    return TypedResults.NotFound("Category to delete not found.");
});
#endregion

#region director endpoints
app.MapGet("/Director/{directorId:int}", async Task<Results<Ok<DirectorViewWithFilmsDTO>, NotFound<string>>> (FilmCatalogContext context, int directorId) =>
    EntityToDTOMappers.MapDirectorWithFilms(await context.Directors
        .Include(c => c.Films).ThenInclude(f => f.Format)
        .Include(c => c.Films).ThenInclude(f => f.Categories)
        .Include(c => c.Films).ThenInclude(f => f.Actors)
    .SingleOrDefaultAsync(d => d.DirectorId == directorId))
    is DirectorViewWithFilmsDTO director && director.DirectorId > 0
        ? TypedResults.Ok(director)
        : TypedResults.NotFound("Director not found."));

app.MapGet("/Director", Results<Ok<IEnumerable<DirectorViewWithFilmsDTO>>, NotFound<string>> (FilmCatalogContext context) =>
    EntityToDTOMappers.MapDirectorsWithFilms(context.Directors
        .Include(c => c.Films).ThenInclude(f => f.Format)
        .Include(c => c.Films).ThenInclude(f => f.Categories)
        .Include(c => c.Films).ThenInclude(f => f.Actors))
    is IEnumerable<DirectorViewWithFilmsDTO> directors
        ? TypedResults.Ok(directors)
        : TypedResults.NotFound("Directors not found."));

app.MapPost("/Director", async Task<Results<BadRequest<string>, Created<Director>>> (FilmCatalogContext context, DirectorCreateDTO createDirector) =>
{
    (bool IsValid, string ErrorMessage) = createDirector.Validate();
    if (!IsValid)
    {
        return TypedResults.BadRequest(ErrorMessage);
    }

    Director? directorToCreate = DTOToEntityMappers.MapDirectorCreate(createDirector);

    if (directorToCreate is not null)
    {
        context.Directors.Add(directorToCreate);
        await context.SaveChangesAsync();
        return TypedResults.Created($"/Director/{directorToCreate.DirectorId}", directorToCreate);
    }

    return TypedResults.BadRequest("Unable to create director.");
});

app.MapPut("/Director/{directorId:int}", (FilmCatalogContext context, DirectorUpdateDTO updateDirector, int directorId) =>
{

});

app.MapDelete("/Director/{directorId:int}", async Task<Results<BadRequest<string>, NoContent, NotFound<string>>> (FilmCatalogContext context, int directorId) =>
{
    if (directorId < 1)
    {
        return TypedResults.BadRequest("Invalid director id.");
    }

    Director directorToDelete = await context.Directors.SingleOrDefaultAsync(d => d.DirectorId == directorId);

    if (directorToDelete is not null)
    {
        context.Directors.Remove(directorToDelete);
        await context.SaveChangesAsync();
        return TypedResults.NoContent();
    }

    return TypedResults.NotFound("Director to delete not found.");
});
#endregion

#region film endpoints
app.MapGet("/Film/{filmId:int}", async Task<Results<Ok<FilmViewDTO>, NotFound<string>>> (FilmCatalogContext context, int filmId) => EntityToDTOMappers.MapFilm(await context.Films.Include(f => f.Director).Include(f => f.Format).Include(f => f.Categories).Include(f => f.Actors).SingleOrDefaultAsync(f => f.FilmId == filmId)) is FilmViewDTO film && film.FilmId > 0
        ? TypedResults.Ok(film)
        : TypedResults.NotFound("Film not found."));

app.MapGet("/Film", Results<Ok<IEnumerable<FilmViewDTO>>, NotFound<string>> (FilmCatalogContext context) => EntityToDTOMappers.MapFilms(context.Films.Include(f => f.Director).Include(f => f.Format).Include(f => f.Categories).Include(f => f.Actors)) is IEnumerable<FilmViewDTO> films
        ? TypedResults.Ok(films)
        : TypedResults.NotFound("Films not found."));

app.MapPost("/Film", async Task<Results<BadRequest<string>, Created<FilmViewDTO>>> (FilmCatalogContext context, FilmCreateDTO createFilm) =>
{
    (bool IsValid, string ErrorMessage) = createFilm.Validate();
    if (!IsValid)
    {
        return TypedResults.BadRequest(ErrorMessage);
    }

    Film? filmToCreate = DTOToEntityMappers.MapFilmCreate(createFilm);

    if (filmToCreate is not null)
    {
        foreach (Actor actor in filmToCreate.Actors)
        {
            if (actor.ActorId > 0)
            {
                context.Actors.Attach(actor);
            }
        }
        foreach (Category category in filmToCreate.Categories)
        {
            if (category.CategoryId > 0)
            {
                context.Categories.Attach(category);
            }
        }

        context.Films.Add(filmToCreate);
        await context.SaveChangesAsync();

        Film? createdFilm = await context.Films.Include(f => f.Director).Include(f => f.Format).Include(f => f.Categories).Include(f => f.Actors).SingleOrDefaultAsync(f => f.FilmId == filmToCreate.FilmId);

        return TypedResults.Created($"/Film/{createdFilm.FilmId}", EntityToDTOMappers.MapFilm(createdFilm));
    }

    return TypedResults.BadRequest("Unable to create film.");
});

app.MapPut("/Film/{filmId:int}", (FilmCatalogContext context, FilmUpdateDTO updateFilm, int filmId) =>
{

});

app.MapDelete("/Film/{filmId:int}", async Task<Results<BadRequest<string>, NoContent, NotFound<string>>> (FilmCatalogContext context, int filmId) =>
{
    if (filmId < 1)
    {
        return TypedResults.BadRequest("Invalid film id.");
    }

    Film filmToDelete = await context.Films.SingleOrDefaultAsync(f => f.FilmId == filmId);

    if (filmToDelete is not null)
    {
        context.Films.Remove(filmToDelete);
        await context.SaveChangesAsync();
        return TypedResults.NoContent();
    }

    return TypedResults.NotFound("Film to delete not found.");
});
#endregion

#region format endpoints
app.MapGet("/Format/{formatId:int}", async Task<Results<Ok<FormatViewWithFilmsDTO>, NotFound<string>>> (FilmCatalogContext context, int formatId) =>
    EntityToDTOMappers.MapFormatWithFilms(await context.Formats
        .Include(c => c.Films).ThenInclude(f => f.Director)
        .Include(c => c.Films).ThenInclude(f => f.Categories)
        .Include(c => c.Films).ThenInclude(f => f.Actors)
    .SingleOrDefaultAsync(f => f.FormatId == formatId))
    is FormatViewWithFilmsDTO format && format.FormatId > 0
        ? TypedResults.Ok(format)
        : TypedResults.NotFound("Format not found."));

app.MapGet("/Format", Results<Ok<IEnumerable<FormatViewWithFilmsDTO>>, NotFound<string>> (FilmCatalogContext context) =>
    EntityToDTOMappers.MapFormatsWithFilms(context.Formats
        .Include(c => c.Films).ThenInclude(f => f.Director)
        .Include(c => c.Films).ThenInclude(f => f.Categories)
        .Include(c => c.Films).ThenInclude(f => f.Actors))
    is IEnumerable<FormatViewWithFilmsDTO> formats
        ? TypedResults.Ok(formats)
        : TypedResults.NotFound("Formats not found."));

app.MapPost("/Format", async Task<Results<BadRequest<string>, Created<Format>>> (FilmCatalogContext context, FormatCreateDTO createFormat) =>
{
    (bool IsValid, string ErrorMessage) = createFormat.Validate();
    if (!IsValid)
    {
        return TypedResults.BadRequest(ErrorMessage);
    }

    Format? formatToCreate = DTOToEntityMappers.MapFormatCreate(createFormat);

    if (formatToCreate is not null)
    {
        context.Formats.Add(formatToCreate);
        await context.SaveChangesAsync();
        return TypedResults.Created($"/Format/{formatToCreate.FormatId}", formatToCreate);
    }

    return TypedResults.BadRequest("Unable to create format.");
});

app.MapPut("/Format/{formatId:int}", (FilmCatalogContext context, FormatUpdateDTO updateFormat, int formatId) =>
{

});

app.MapDelete("/Format/{formatId:int}", async Task<Results<BadRequest<string>, NoContent, NotFound<string>>> (FilmCatalogContext context, int formatId) =>
{
    if (formatId < 1)
    {
        return TypedResults.BadRequest("Invalid format id.");
    }

    Format formatToDelete = await context.Formats.SingleOrDefaultAsync(f => f.FormatId == formatId);

    if (formatToDelete is not null)
    {
        context.Formats.Remove(formatToDelete);
        await context.SaveChangesAsync();
        return TypedResults.NoContent();
    }

    return TypedResults.NotFound("Format to delete not found.");
});
#endregion

app.Run();
