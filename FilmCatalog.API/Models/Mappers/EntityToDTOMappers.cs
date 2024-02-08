using FilmCatalog.API.Models.DTOs;
using FilmCatalog.API.Models.Entities;
using System.Collections.Immutable;
using Actor = FilmCatalog.API.Models.Entities.Actor;
using Format = FilmCatalog.API.Models.Entities.Format;

namespace FilmCatalog.API.Models.Mappers
{
    public static class EntityToDTOMappers
    {
        public static ActorViewDTO MapActor(Actor actor) => actor == null ? ActorViewDTO.NotFound : new() { ActorId = actor.ActorId, Name = actor.Name, Films = MapFilms(actor.Films) };

        public static ImmutableList<ActorViewDTO> MapActors(IEnumerable<Actor> actors)
        {
            ImmutableList<ActorViewDTO> dtoList = [];

            if (actors == null) { return dtoList; }

            foreach (Actor actor in actors)
            {
                dtoList.Add(MapActor(actor));
            }
            return dtoList;
        }

        public static CategoryViewDTO MapCategory(Category category) => category == null ? CategoryViewDTO.NotFound : new() { CategoryId = category.CategoryId, CategoryName = category.CategoryName, Films = MapFilms(category.Films) };

        public static ImmutableList<CategoryViewDTO> MapCategories(IEnumerable<Category> categories)
        {
            ImmutableList<CategoryViewDTO> dtoList = [];

            if (categories == null) { return dtoList; }

            foreach (Category category in categories)
            {
                dtoList.Add(MapCategory(category));
            }
            return dtoList;
        }

        public static DirectorViewDTO MapDirector(Director director) => director == null ? DirectorViewDTO.NotFound : new() { DirectorId = director.DirectorId, Name = director.Name, Films = MapFilms(director.Films) };

        public static ImmutableList<DirectorViewDTO> MapDirectors(IEnumerable<Director> directors)
        {
            ImmutableList<DirectorViewDTO> dtoList = [];

            if (directors == null) { return dtoList; }

            foreach (Director director in directors)
            {
                dtoList.Add(MapDirector(director));
            }
            return dtoList;
        }

        public static FilmViewDTO MapFilm(Film film) => film == null ? FilmViewDTO.NotFound : new() { FilmId = film.FilmId, Title = film.Title, Description = film.Description, DirectorId = film.DirectorId, FormatId = film.FormatId, Quantity = film.Quantity, Year = film.Year, Studio = film.Studio, IsFavorite = film.IsFavorite, IsRareCollectibleAndOrValuable = film.IsRareCollectibleAndOrValuable, CreateDate = film.CreateDate, UpdateDate = film.UpdateDate, Director = film.Director == null ? null : MapDirector(film.Director), Format = MapFormat(film.Format), Actors = MapActors(film.Actors), Categories = MapCategories(film.Categories) };

        public static ImmutableList<FilmViewDTO> MapFilms(IEnumerable<Film> films)
        {
            ImmutableList<FilmViewDTO> dtoList = [];

            if (films == null) { return dtoList; }

            foreach (Film film in films)
            {
                dtoList.Add(MapFilm(film));
            }
            return dtoList;
        }

        public static FormatViewDTO MapFormat(Format format) => format == null ? FormatViewDTO.NotFound : new() { FormatId = format.FormatId, FormatName = format.FormatName, Films = MapFilms(format.Films) };

        public static ImmutableList<FormatViewDTO> MapFormats(IEnumerable<Format> formats)
        {
            ImmutableList<FormatViewDTO> dtoList = [];

            if (formats == null) { return dtoList; }

            foreach (Format format in formats)
            {
                dtoList.Add(MapFormat(format));
            }
            return dtoList;
        }
    }
}
