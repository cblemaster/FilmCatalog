using FilmCatalog.API.Models.DTOs;
using FilmCatalog.API.Models.Entities;
using Actor = FilmCatalog.API.Models.Entities.Actor;
using Format = FilmCatalog.API.Models.Entities.Format;

namespace FilmCatalog.API.Models.Mappers
{
    public static class EntityToDTOMappers
    {
        public static ActorViewDTO MapActor(Actor actor) => actor == null ? ActorViewDTO.NotFound : new() { ActorId = actor.ActorId, Name = actor.Name, Films = MapFilmsForNonFilmTypes(actor.Films) };

        public static ActorViewForFilmDTO MapActorForFilm(Actor actor) => actor == null ? ActorViewForFilmDTO.NotFound : new() { ActorId = actor.ActorId, Name = actor.Name };

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

        public static IEnumerable<ActorViewForFilmDTO> MapActorsForFilm(IEnumerable<Actor> actors)
        {
            List<ActorViewForFilmDTO> dtoList = [];

            if (actors == null) { return dtoList; }

            foreach (Actor actor in actors)
            {
                dtoList.Add(MapActorForFilm(actor));
            }
            return dtoList;
        }

        public static CategoryViewDTO MapCategory(Category category) => category == null ? CategoryViewDTO.NotFound : new() { CategoryId = category.CategoryId, CategoryName = category.CategoryName, Films = MapFilmsForNonFilmTypes(category.Films) };

        public static CategoryViewForFilmDTO MapCategoryForFilm(Category category) => category == null ? CategoryViewForFilmDTO.NotFound : new() { CategoryId = category.CategoryId, CategoryName = category.CategoryName };

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

        public static IEnumerable<CategoryViewForFilmDTO> MapCategoriesForFilm(IEnumerable<Category> categories)
        {
            List<CategoryViewForFilmDTO> dtoList = [];

            if (categories == null) { return dtoList; }

            foreach (Category category in categories)
            {
                dtoList.Add(MapCategoryForFilm(category));
            }
            return dtoList;
        }

        public static DirectorViewDTO MapDirector(Director director) => director == null ? DirectorViewDTO.NotFound : new() { DirectorId = director.DirectorId, Name = director.Name, Films = director.Films == null ? Enumerable.Empty<FilmViewForNonFilmTypesDTO>() : MapFilmsForNonFilmTypes(director.Films) };

        public static DirectorViewForFilmDTO MapDirectorForFilm(Director director) => director == null ? DirectorViewForFilmDTO.NotFound : new() { DirectorId = director.DirectorId, Name = director.Name };

        public static IEnumerable<DirectorViewDTO> MapDirectors(IEnumerable<Director> directors)
        {
            List<DirectorViewDTO> dtoList = [];

            if (directors == null) { return dtoList; }

            foreach (Director director in directors)
            {
                dtoList.Add(MapDirector(director));
            }
            return dtoList;
        }

        public static FilmViewDTO MapFilm(Film film) => film == null ? FilmViewDTO.NotFound : new() { FilmId = film.FilmId, Title = film.Title, Description = film.Description, DirectorId = film.DirectorId, FormatId = film.FormatId, Quantity = film.Quantity, Year = film.Year, Studio = film.Studio, IsFavorite = film.IsFavorite, IsRareCollectibleAndOrValuable = film.IsRareCollectibleAndOrValuable, CreateDate = film.CreateDate, UpdateDate = film.UpdateDate, Director = film.Director == null ? null : MapDirectorForFilm(film.Director), Format = MapFormatForFilm(film.Format), Actors = MapActorsForFilm(film.Actors), Categories = MapCategoriesForFilm(film.Categories) };

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

        public static FormatViewDTO MapFormat(Format format) => format == null ? FormatViewDTO.NotFound : new() { FormatId = format.FormatId, FormatName = format.FormatName, Films = MapFilmsForNonFilmTypes(format.Films) };

        public static FormatViewForFilmDTO MapFormatForFilm(Format format) => format == null ? FormatViewForFilmDTO.NotFound : new() { FormatId = format.FormatId, FormatName = format.FormatName };

        public static IEnumerable<FormatViewDTO> MapFormats(IEnumerable<Format> formats)
        {
            List<FormatViewDTO> dtoList = [];

            if (formats == null) { return dtoList; }

            foreach (Format format in formats)
            {
                dtoList.Add(MapFormat(format));
            }
            return dtoList;
        }
    }
}
