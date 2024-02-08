using FilmCatalog.API.Models.DTOs;
using FilmCatalog.API.Models.Entities;

namespace FilmCatalog.API.Models.Mappers
{
    public static class DTOToEntityMappers
    {
        public static Actor? MapActorCreate(ActorCreateDTO createActor) => createActor == null ? null : new() { Name = createActor.Name };

        public static Actor? MapActorUpdate(ActorUpdateDTO updateActor) => updateActor == null ? null : new() { ActorId = updateActor.ActorId, Name = updateActor.Name, Films = MapFilmViewCollection(updateActor.Films).ToList() };

        public static Actor MapActorView(ActorViewForFilmDTO actor) => actor == null ? new() : new() { ActorId = actor.ActorId, Name = actor.Name };

        public static IEnumerable<Actor> MapActorViewCollection(IEnumerable<ActorViewForFilmDTO> actors)
        {
            List<Actor> entityList = [];

            if (actors == null) { return entityList; }

            foreach (ActorViewForFilmDTO actor in actors)
            {
                if (MapActorView(actor) is Actor mappedActor)
                {
                    entityList.Add(mappedActor);
                }
            }
            return entityList;
        }

        public static Category? MapCategoryCreate(CategoryCreateDTO createCategory) => createCategory == null ? null : new() { CategoryName = createCategory.CategoryName };

        public static Category? MapCategoryUpdate(CategoryUpdateDTO updateCategory) => updateCategory == null ? null : new() { CategoryId = updateCategory.CategoryId, CategoryName = updateCategory.CategoryName, Films = MapFilmViewCollection(updateCategory.Films).ToList() };

        public static Category MapCategoryView(CategoryViewForFilmDTO category) => category == null ? new() : new() { CategoryId = category.CategoryId, CategoryName = category.CategoryName };

        public static IEnumerable<Category> MapCategoryViewCollection(IEnumerable<CategoryViewForFilmDTO> categories)
        {
            List<Category> entityList = [];

            if (categories == null) { return entityList; }

            foreach (CategoryViewForFilmDTO category in categories)
            {
                if (MapCategoryView(category) is Category mappedCategory)
                {
                    entityList.Add(mappedCategory);
                }
            }
            return entityList;
        }

        public static Director? MapDirectorCreate(DirectorCreateDTO createDirector) => createDirector == null ? null : new() { Name = createDirector.Name };

        public static Director? MapDirectorUpdate(DirectorUpdateDTO updateDirector) => updateDirector == null ? null : new() { DirectorId = updateDirector.DirectorId, Name = updateDirector.Name, Films = MapFilmViewCollection(updateDirector.Films).ToList() };

        public static Director? MapDirectorView(DirectorViewForFilmDTO director) => director == null ? null : new() { DirectorId = director.DirectorId, Name = director.Name };

        public static Film? MapFilmCreate(FilmCreateDTO createFilm) => createFilm == null ? null : new() { Title = createFilm.Title, Description = createFilm.Description, DirectorId = createFilm.DirectorId, FormatId = createFilm.FormatId, Quantity = createFilm.Quantity, Year = createFilm.Year, Studio = createFilm.Studio, IsFavorite = createFilm.IsFavorite, IsRareCollectibleAndOrValuable = createFilm.IsRareCollectibleAndOrValuable, CreateDate = createFilm.CreateDate, Actors = MapActorViewCollection(createFilm.Actors).ToList(), Categories = MapCategoryViewCollection(createFilm.Categories).ToList() };

        public static Film? MapFilmUpdate(FilmUpdateDTO updateFilm) => updateFilm == null ? null : new() { FilmId = updateFilm.FilmId, Title = updateFilm.Title, Description = updateFilm.Description, DirectorId = updateFilm.DirectorId, FormatId = updateFilm.FormatId, Quantity = updateFilm.Quantity, Year = updateFilm.Year, Studio = updateFilm.Studio, IsFavorite = updateFilm.IsFavorite, IsRareCollectibleAndOrValuable = updateFilm.IsRareCollectibleAndOrValuable, UpdateDate = updateFilm.UpdateDate, Actors = MapActorViewCollection(updateFilm.Actors).ToList(), Categories = MapCategoryViewCollection(updateFilm.Categories).ToList() };

        public static Film? MapFilmView(FilmViewDTO film) => film == null ? null : new() { FilmId = film.FilmId, Title = film.Title, Description = film.Description, DirectorId = film.DirectorId, FormatId = film.FormatId, Quantity = film.Quantity, Year = film.Year, Studio = film.Studio, IsFavorite = film.IsFavorite, IsRareCollectibleAndOrValuable = film.IsRareCollectibleAndOrValuable, CreateDate = film.CreateDate, UpdateDate = film.UpdateDate, Actors = MapActorViewCollection(film.Actors).ToList(), Categories = MapCategoryViewCollection(film.Categories).ToList(), Director = film.Director == null ? null : MapDirectorView(film.Director), Format = MapFormatView(film.Format) };

        public static IEnumerable<Film> MapFilmViewCollection(IEnumerable<FilmViewDTO> films)
        {
            List<Film> entityList = [];

            if (films == null) { return entityList; }

            foreach (FilmViewDTO film in films)
            {
                if (MapFilmView(film) is Film mappedFilm)
                {
                    entityList.Add(mappedFilm);
                }
            }
            return entityList;
        }

        public static Format? MapFormatCreate(FormatCreateDTO createFormat) => createFormat == null ? null : new() { FormatName = createFormat.FormatName };

        public static Format? MapFormatUpdate(FormatUpdateDTO updateFormat) => updateFormat == null ? null : new() { FormatId = updateFormat.FormatId, FormatName = updateFormat.FormatName, Films = MapFilmViewCollection(updateFormat.Films).ToList() };

        public static Format MapFormatView(FormatViewForFilmDTO format) => format == null ? new() : new() { FormatId = format.FormatId, FormatName = format.FormatName };
    }
}
