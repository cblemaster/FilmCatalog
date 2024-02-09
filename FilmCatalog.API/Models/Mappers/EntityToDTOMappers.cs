using FilmCatalog.API.Models.DTOs;
using FilmCatalog.API.Models.Entities;
using Actor = FilmCatalog.API.Models.Entities.Actor;
using Format = FilmCatalog.API.Models.Entities.Format;

namespace FilmCatalog.API.Models.Mappers
{
    public static class EntityToDTOMappers
    {
        public static ActorViewWithFilmsDTO MapActorWithFilms(Actor actor) => actor == null
            ? ActorViewWithFilmsDTO.NotFound
            : new() { ActorId = actor.ActorId, Name = actor.Name, Films = MapFilms(actor.Films) };

        public static ActorViewDTO MapActor(Actor actor) => actor == null
            ? ActorViewDTO.NotFound
            : new() { ActorId = actor.ActorId, Name = actor.Name };

        public static IEnumerable<ActorViewWithFilmsDTO> MapActorsWithFilms(IEnumerable<Actor> actors)
        {
            List<ActorViewWithFilmsDTO> dtoList = [];

            if (actors == null) { return dtoList; }

            foreach (Actor actor in actors)
            {
                dtoList.Add(MapActorWithFilms(actor));
            }
            return dtoList;
        }

        public static IEnumerable<ActorViewDTO> MapActors(IEnumerable<Actor> actors)
        {
            List<ActorViewDTO> dtoList = [];

            if (actors == null) { return dtoList; }

            foreach (Actor actor in actors)
            {
                dtoList.Add(MapActor(actor));
            }
            return dtoList;
        }

        public static CategoryViewWithFilmsDTO MapCategoryWithFilms(Category category) => category == null
            ? CategoryViewWithFilmsDTO.NotFound
            : new() { CategoryId = category.CategoryId, CategoryName = category.CategoryName, Films = MapFilms(category.Films) };

        public static CategoryViewDTO MapCategory(Category category) => category == null
            ? CategoryViewDTO.NotFound
            : new() { CategoryId = category.CategoryId, CategoryName = category.CategoryName };

        public static IEnumerable<CategoryViewWithFilmsDTO> MapCategoriesWithFilms(IEnumerable<Category> categories)
        {
            List<CategoryViewWithFilmsDTO> dtoList = [];

            if (categories == null) { return dtoList; }

            foreach (Category category in categories)
            {
                dtoList.Add(MapCategoryWithFilms(category));
            }
            return dtoList;
        }

        public static IEnumerable<CategoryViewDTO> MapCategories(IEnumerable<Category> categories)
        {
            List<CategoryViewDTO> dtoList = [];

            if (categories == null) { return dtoList; }

            foreach (Category category in categories)
            {
                dtoList.Add(MapCategory(category));
            }
            return dtoList;
        }

        public static DirectorViewWithFilmsDTO MapDirectorWithFilms(Director director) => director == null
            ? DirectorViewWithFilmsDTO.NotFound
            : new() { DirectorId = director.DirectorId, Name = director.Name, Films = MapFilms(director.Films) };

        public static DirectorViewDTO MapDirector(Director director) => director == null
            ? DirectorViewDTO.NotFound
            : new() { DirectorId = director.DirectorId, Name = director.Name };

        public static IEnumerable<DirectorViewWithFilmsDTO> MapDirectorsWithFilms(IEnumerable<Director> directors)
        {
            List<DirectorViewWithFilmsDTO> dtoList = [];

            if (directors == null) { return dtoList; }

            foreach (Director director in directors)
            {
                dtoList.Add(MapDirectorWithFilms(director));
            }
            return dtoList;
        }

        public static FilmViewDTO MapFilm(Film film) => film == null ? FilmViewDTO.NotFound : new() { FilmId = film.FilmId, Title = film.Title, Description = film.Description, DirectorId = film.DirectorId, FormatId = film.FormatId, Quantity = film.Quantity, Year = film.Year, Studio = film.Studio, IsFavorite = film.IsFavorite, IsRareCollectibleAndOrValuable = film.IsRareCollectibleAndOrValuable, CreateDate = film.CreateDate, UpdateDate = film.UpdateDate, Director = film.Director == null ? null : MapDirector(film.Director), Format = MapFormat(film.Format), Actors = MapActors(film.Actors), Categories = MapCategories(film.Categories) };

        public static FilmViewForNonFilmTypesDTO MapFilmForNonFilmTypes(Film film) => film == null ? FilmViewForNonFilmTypesDTO.NotFound : new() { FilmId = film.FilmId, Title = film.Title, Description = film.Description, Quantity = film.Quantity, Year = film.Year, Studio = film.Studio, IsFavorite = film.IsFavorite, IsRareCollectibleAndOrValuable = film.IsRareCollectibleAndOrValuable, CreateDate = film.CreateDate, UpdateDate = film.UpdateDate };

        public static IEnumerable<FilmViewDTO> MapFilms(IEnumerable<Film> films)
        {
            List<FilmViewDTO> dtoList = [];

            if (films == null) { return dtoList; }

            foreach (Film film in films)
            {
                dtoList.Add(MapFilm(film));
            }
            return dtoList;
        }

        public static IEnumerable<FilmViewForNonFilmTypesDTO> MapFilmsForNonFilmTypes(IEnumerable<Film> films)
        {
            List<FilmViewForNonFilmTypesDTO> dtoList = [];

            if (films == null) { return dtoList; }

            foreach (Film film in films)
            {
                dtoList.Add(MapFilmForNonFilmTypes(film));
            }
            return dtoList;
        }

        public static FormatViewWithFilmsDTO MapFormatWithFilms(Format format) => format == null
            ? FormatViewWithFilmsDTO.NotFound
            : new() { FormatId = format.FormatId, FormatName = format.FormatName, Films = MapFilms(format.Films) };

        public static FormatViewDTO MapFormat(Format format) => format == null
            ? FormatViewDTO.NotFound
            : new() { FormatId = format.FormatId, FormatName = format.FormatName };

        public static IEnumerable<FormatViewWithFilmsDTO> MapFormatsWithFilms(IEnumerable<Format> formats)
        {
            List<FormatViewWithFilmsDTO> dtoList = [];

            if (formats == null) { return dtoList; }

            foreach (Format format in formats)
            {
                dtoList.Add(MapFormatWithFilms(format));
            }
            return dtoList;
        }
    }
}
