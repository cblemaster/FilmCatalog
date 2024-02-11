using FilmCatalog.API.Models.DTOs;
using FilmCatalog.API.Models.Entities;

namespace FilmCatalog.API.Models.Mappers
{
    public static class EntityToDTOMappers
    {
        public static DisplayActor MapActor(Actor actor) =>
            actor == null
                ? DisplayActor.NotFound
                : new()
                {
                    ActorId = actor.ActorId,
                    Name = actor.Name,
                };

        public static IEnumerable<DisplayActor> MapActorCollection(IEnumerable<Actor> actors)
        {
            if (actors == null || actors.Any())
            {
                return Enumerable.Empty<DisplayActor>().ToList();
            }

            List<DisplayActor> result = [];

            foreach (Actor actor in actors)
            {
                result.Add(MapActor(actor));
            }

            return result;
        }

        public static DisplayCategory MapCategory(Category category) =>
            category == null
                ? DisplayCategory.NotFound
                : new()
                {
                    CategoryId = category.CategoryId,
                    CategoryName = category.CategoryName,
                };

        public static IEnumerable<DisplayCategory> MapCategoryCollection(IEnumerable<Category> categories)
        {
            if (categories == null || categories.Any())
            {
                return Enumerable.Empty<DisplayCategory>().ToList();
            }

            List<DisplayCategory> result = [];

            foreach (Category category in categories)
            {
                result.Add(MapCategory(category));
            }

            return result;
        }

        public static DisplayDirector MapDirector(Director director) =>
            director == null
                ? DisplayDirector.NotFound
                : new()
                {
                    DirectorId = director.DirectorId,
                    Name = director.Name,
                };

        public static IEnumerable<DisplayDirector> MapDirectorCollection(IEnumerable<Director> directors)
        {
            if (directors == null || directors.Any())
            {
                return Enumerable.Empty<DisplayDirector>().ToList();
            }

            List<DisplayDirector> result = [];

            foreach (Director director in directors)
            {
                result.Add(MapDirector(director));
            }

            return result;
        }

        public static DisplayFilm MapFilm(Film film) =>
            film == null
                ? DisplayFilm.NotFound
                : new()
                {
                    FilmId = film.FilmId,
                    Title = film.Title,
                    Description = film.Description,
                    FormatId = film.FormatId,
                    DirectorId = film.DirectorId,
                    Quantity = film.Quantity,
                    Year = film.Year,
                    Studio = film.Studio,
                    IsFavorite = film.IsFavorite,
                    IsRareCollectibleAndOrValuable = film.IsRareCollectibleAndOrValuable,
                    StarRating = film.StarRating,
                    CreateDate = film.CreateDate,
                    UpdateDate = film.UpdateDate,
                    Format = MapFormat(film.Format),
                    Director = film.Director == null ? null : MapDirector(film.Director),
                    Actors = MapActorCollection(film.Actors),
                    Categories = MapCategoryCollection(film.Categories),
                };

        public static IEnumerable<DisplayFilm> MapFilmCollection(IEnumerable<Film> films)
        {
            if (films == null || films.Any())
            {
                return Enumerable.Empty<DisplayFilm>().ToList();
            }

            List<DisplayFilm> result = [];

            foreach (Film film in films)
            {
                result.Add(MapFilm(film));
            }

            return result;
        }

        public static DisplayFormat MapFormat(Format format) =>
            format == null
                ? DisplayFormat.NotFound
                : new()
                {
                    FormatId = format.FormatId,
                    FormatName = format.FormatName,
                };

        public static IEnumerable<DisplayFormat> MapFormatCollection(IEnumerable<Format> formats)
        {
            if (formats == null || formats.Any())
            {
                return Enumerable.Empty<DisplayFormat>().ToList();
            }

            List<DisplayFormat> result = [];

            foreach (Format format in formats)
            {
                result.Add(MapFormat(format));
            }

            return result;
        }
    }
}
