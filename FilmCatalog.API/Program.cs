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

app.MapGet("/actor", Results<Ok<IEnumerable<DisplayActor>>, NotFound<string>> (FilmCatalogContext context) =>
{
    if (context.Actors is IEnumerable<Actor> actors && actors.Any())
    {
        IEnumerable<DisplayActor> displayActors = EntityToDTOMappers.MapActorCollection(actors);
        return TypedResults.Ok(displayActors);
    }
    return TypedResults.NotFound("No actors found.");
});

app.MapPost("/actor", async Task<Results<BadRequest<string>, Created<DisplayActor>>> (FilmCatalogContext context, CreateActor createActor) =>
{
    if (createActor is null)
    {
        return TypedResults.BadRequest("No actor to create provided.");
    }

    (bool IsValid, string ErrorMessage) = createActor.Validate();

    if (!IsValid)
    {
        return TypedResults.BadRequest(ErrorMessage);
    }

    Actor actorToCreate = DTOToEntityMappers.MapCreateActor(createActor);
    context.Actors.Add(actorToCreate);
    await context.SaveChangesAsync();

    return TypedResults.Created($"/actor/{actorToCreate.ActorId}", EntityToDTOMappers.MapActor(actorToCreate));
});

app.MapPut("/actor/{actorId:int}", async Task<Results<BadRequest<string>, NoContent>> (FilmCatalogContext context, RenameActor renameActor, int actorId) =>
{
    if (renameActor is null)
    {
        return TypedResults.BadRequest("No actor to rename provided.");
    }

    if (actorId < 1 || actorId != renameActor.ActorId)
    {
        return TypedResults.BadRequest("Invalid actor id.");
    }

    (bool IsValid, string ErrorMessage) = renameActor.Validate();

    if (!IsValid)
    {
        return TypedResults.BadRequest(ErrorMessage);
    }

    Actor actorToUpdate = DTOToEntityMappers.MapRenameActor(renameActor);
    context.Actors.Entry(actorToUpdate).State = EntityState.Modified;
    await context.SaveChangesAsync();

    return TypedResults.NoContent();
});

app.MapDelete("/actor/{actorId:int}", async Task<Results<BadRequest<string>, NoContent, NotFound<string>>> (FilmCatalogContext context, int actorId) =>
{
    if (actorId < 1)
    {
        return TypedResults.BadRequest("Invalid actor id.");
    }

    if (await context.Actors.Include(c => c.Films).SingleOrDefaultAsync(a => a.ActorId == actorId) is Actor actor)
    {
        if (actor.Films.Any())
        {
            return TypedResults.BadRequest("Unable to delete actor because s/he is associated with one or more films.");
        }

        context.Actors.Remove(actor);
        await context.SaveChangesAsync();

        return TypedResults.NoContent();
    }

    return TypedResults.NotFound("Unable to find actor to delete.");
});
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

app.MapGet("/category", Results<Ok<IEnumerable<DisplayCategory>>, NotFound<string>> (FilmCatalogContext context) =>
{
    if (context.Categories is IEnumerable<Category> categories && categories.Any())
    {
        IEnumerable<DisplayCategory> displayCategories = EntityToDTOMappers.MapCategoryCollection(categories);
        return TypedResults.Ok(displayCategories);
    }
    return TypedResults.NotFound("No categories found.");
});

app.MapPost("/category", async Task<Results<BadRequest<string>, Created<DisplayCategory>>> (FilmCatalogContext context, CreateCategory createCategory) =>
{
    if (createCategory is null)
    {
        return TypedResults.BadRequest("No category to create provided.");
    }

    (bool IsValid, string ErrorMessage) = createCategory.Validate();

    if (!IsValid)
    {
        return TypedResults.BadRequest(ErrorMessage);
    }

    Category categoryToCreate = DTOToEntityMappers.MapCreateCategory(createCategory);
    context.Categories.Add(categoryToCreate);
    await context.SaveChangesAsync();

    return TypedResults.Created($"/category/{categoryToCreate.CategoryId}", EntityToDTOMappers.MapCategory(categoryToCreate));
});

app.MapDelete("/category/{categoryId:int}", async Task<Results<BadRequest<string>, NoContent, NotFound<string>>> (FilmCatalogContext context, int categoryId) =>
{
    if (categoryId < 1)
    {
        return TypedResults.BadRequest("Invalid category id.");
    }

    if (await context.Categories.Include(c => c.Films).SingleOrDefaultAsync(c => c.CategoryId == categoryId) is Category category)
    {
        if (category.Films.Any())
        {
            return TypedResults.BadRequest("Unable to delete category because it is associated with one or more films.");
        }

        context.Categories.Remove(category);
        await context.SaveChangesAsync();

        return TypedResults.NoContent();
    }

    return TypedResults.NotFound("Unable to find category to delete.");
});
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

app.MapGet("/director", Results<Ok<IEnumerable<DisplayDirector>>, NotFound<string>> (FilmCatalogContext context) =>
{
    if (context.Directors is IEnumerable<Director> directors && directors.Any())
    {
        IEnumerable<DisplayDirector> displayDirectors = EntityToDTOMappers.MapDirectorCollection(directors);
        return TypedResults.Ok(displayDirectors);
    }
    return TypedResults.NotFound("No directors found.");
});

app.MapPost("/director", async Task<Results<BadRequest<string>, Created<DisplayDirector>>> (FilmCatalogContext context, CreateDirector createDirector) =>
{
    if (createDirector is null)
    {
        return TypedResults.BadRequest("No director to create provided.");
    }

    (bool IsValid, string ErrorMessage) = createDirector.Validate();

    if (!IsValid)
    {
        return TypedResults.BadRequest(ErrorMessage);
    }

    Director directorToCreate = DTOToEntityMappers.MapCreateDirector(createDirector);
    context.Directors.Add(directorToCreate);
    await context.SaveChangesAsync();

    return TypedResults.Created($"/director/{directorToCreate.DirectorId}", EntityToDTOMappers.MapDirector(directorToCreate));
});

app.MapPut("/director/{directorId:int}", async Task<Results<BadRequest<string>, NoContent>> (FilmCatalogContext context, RenameDirector renameDirector, int directorId) =>
{
    if (renameDirector is null)
    {
        return TypedResults.BadRequest("No director to rename provided.");
    }

    if (directorId < 1 || directorId != renameDirector.DirectorId)
    {
        return TypedResults.BadRequest("Invalid director id.");
    }

    (bool IsValid, string ErrorMessage) = renameDirector.Validate();

    if (!IsValid)
    {
        return TypedResults.BadRequest(ErrorMessage);
    }

    Director directorToUpdate = DTOToEntityMappers.MapRenameDirector(renameDirector);
    context.Directors.Entry(directorToUpdate).State = EntityState.Modified;
    await context.SaveChangesAsync();

    return TypedResults.NoContent();
});

app.MapDelete("/director/{directorId:int}", async Task<Results<BadRequest<string>, NoContent, NotFound<string>>> (FilmCatalogContext context, int directorId) =>
{
    if (directorId < 1)
    {
        return TypedResults.BadRequest("Invalid director id.");
    }

    if (await context.Directors.Include(c => c.Films).SingleOrDefaultAsync(d => d.DirectorId == directorId) is Director director)
    {
        if (director.Films.Any())
        {
            return TypedResults.BadRequest("Unable to delete director because s/he is associated with one or more films.");
        }

        context.Directors.Remove(director);
        await context.SaveChangesAsync();

        return TypedResults.NoContent();
    }

    return TypedResults.NotFound("Unable to find director to delete.");
});
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
    if (context.Films is IEnumerable<Film> films && films.Any())
    {
        IEnumerable<DisplayFilm> displayFilms = EntityToDTOMappers.MapFilmCollection(films);
        return TypedResults.Ok(displayFilms);
    }
    return TypedResults.NotFound("No films found.");
});

app.MapGet("/film/favorite", Results<Ok<IEnumerable<DisplayFilm>>, NotFound<string>> (FilmCatalogContext context) =>
{
    if (context.Films.Where(f => f.IsFavorite) is IEnumerable<Film> films && films.Any())
    {
        IEnumerable<DisplayFilm> displayFilms = EntityToDTOMappers.MapFilmCollection(films);
        return TypedResults.Ok(displayFilms);
    }
    return TypedResults.NotFound("No favorite films found.");
});

app.MapGet("/film/rare", Results<Ok<IEnumerable<DisplayFilm>>, NotFound<string>> (FilmCatalogContext context) =>
{
    if (context.Films.Where(f => f.IsRareCollectibleAndOrValuable) is IEnumerable<Film> films && films.Any())
    {
        IEnumerable<DisplayFilm> displayFilms = EntityToDTOMappers.MapFilmCollection(films);
        return TypedResults.Ok(displayFilms);
    }
    return TypedResults.NotFound("No rare, collectible, nor valuable films found.");
});

app.MapGet("/film/fivestar", Results<Ok<IEnumerable<DisplayFilm>>, NotFound<string>> (FilmCatalogContext context) =>
{
    if (context.Films.Where(f => f.StarRating == 5) is IEnumerable<Film> films && films.Any())
    {
        IEnumerable<DisplayFilm> displayFilms = EntityToDTOMappers.MapFilmCollection(films);
        return TypedResults.Ok(displayFilms);
    }
    return TypedResults.NotFound("No rare, collectible, nor valuable films found.");
});

app.MapPost("/film", async Task<Results<BadRequest<string>, Created<DisplayFilm>>> (FilmCatalogContext context, CreateFilm createFilm) =>
{
    if (createFilm is null)
    {
        return TypedResults.BadRequest("No film to create provided.");
    }

    (bool IsValid, string ErrorMessage) = createFilm.Validate();

    if (!IsValid)
    {
        return TypedResults.BadRequest(ErrorMessage);
    }

    Film filmToCreate = DTOToEntityMappers.MapCreateFilm(createFilm);
    context.Films.Add(filmToCreate);
    await context.SaveChangesAsync();

    return TypedResults.Created($"/film/{filmToCreate.FilmId}", EntityToDTOMappers.MapFilm(filmToCreate));
});

app.MapPut("/film/{filmId:int}", async Task<Results<BadRequest<string>, NoContent>> (FilmCatalogContext context, UpdateFilm updateFilm, int filmId) =>
{
    if (updateFilm is null)
    {
        return TypedResults.BadRequest("No film to update provided.");
    }

    if (filmId < 1 || filmId != updateFilm.FilmId)
    {
        return TypedResults.BadRequest("Invalid film id.");
    }

    (bool IsValid, string ErrorMessage) = updateFilm.Validate();

    if (!IsValid)
    {
        return TypedResults.BadRequest(ErrorMessage);
    }

    if (await context.Films.IgnoreAutoIncludes().SingleOrDefaultAsync(f => f.FilmId == filmId) is Film filmToUpdate)
    {
        filmToUpdate = DTOToEntityMappers.MapUpdateFilm(filmToUpdate, updateFilm);
        await context.SaveChangesAsync();

        return TypedResults.NoContent();
    }

    return TypedResults.BadRequest("Unable to find film to update.");
});

app.MapPut("/film/{filmId:int}/actor", () => { });
app.MapPut("/film/{filmId:int}/categoty", () => { });

app.MapDelete("/film/{filmId:int}", async Task<Results<BadRequest<string>, NoContent, NotFound<string>>> (FilmCatalogContext context, int filmId) =>
{
    if (filmId < 1)
    {
        return TypedResults.BadRequest("Invalid film id.");
    }

    if (await context.Films.IgnoreAutoIncludes().SingleOrDefaultAsync(f => f.FilmId == filmId) is Film film)
    {
        context.Films.Remove(film);
        await context.SaveChangesAsync();

        return TypedResults.NoContent();
    }

    return TypedResults.NotFound("Unable to find film to delete.");
});
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

app.MapGet("/format", Results<Ok<IEnumerable<DisplayFormat>>, NotFound<string>> (FilmCatalogContext context) =>
{
    if (context.Formats is IEnumerable<Format> formats && formats.Any())
    {
        IEnumerable<DisplayFormat> displayFormats = EntityToDTOMappers.MapFormatCollection(formats);
        return TypedResults.Ok(displayFormats);
    }
    return TypedResults.NotFound("No formats found.");
});

app.MapPost("/format", async Task<Results<BadRequest<string>, Created<DisplayFormat>>> (FilmCatalogContext context, CreateFormat createFormat) =>
{
    if (createFormat is null)
    {
        return TypedResults.BadRequest("No format to create provided.");
    }

    (bool IsValid, string ErrorMessage) = createFormat.Validate();

    if (!IsValid)
    {
        return TypedResults.BadRequest(ErrorMessage);
    }

    Format formatToCreate = DTOToEntityMappers.MapCreateFormat(createFormat);
    context.Formats.Add(formatToCreate);
    await context.SaveChangesAsync();

    return TypedResults.Created($"/format/{formatToCreate.FormatId}", EntityToDTOMappers.MapFormat(formatToCreate));
});

app.MapDelete("/format/{formatId:int}", async Task<Results<BadRequest<string>, NoContent, NotFound<string>>> (FilmCatalogContext context, int formatId) =>
{
    if (formatId < 1)
    {
        return TypedResults.BadRequest("Invalid format id.");
    }

    if (await context.Formats.Include(f => f.Films).SingleOrDefaultAsync(f => f.FormatId == formatId) is Format format)
    {
        if (format.Films.Any())
        {
            return TypedResults.BadRequest("Unable to delete format because it is associated with one or more films.");
        }

        context.Formats.Remove(format);
        await context.SaveChangesAsync();

        return TypedResults.NoContent();
    }

    return TypedResults.NotFound("Unable to find format to delete.");
});
#endregion

app.Run();
