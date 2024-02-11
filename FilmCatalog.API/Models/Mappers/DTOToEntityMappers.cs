using FilmCatalog.API.Models.DTOs;
using FilmCatalog.API.Models.Entities;

namespace FilmCatalog.API.Models.Mappers
{
    public static class DTOToEntityMappers
    {
        public static Actor MapCreateActor(CreateActor createActor) =>
            createActor == null
                ? Actor.NotFound
                : new()
                {
                    Name = createActor.Name,
                };

        public static Category MapCreateCategory(CreateCategory createCategory) =>
            createCategory == null
                ? Category.NotFound
                : new()
                {
                    CategoryName = createCategory.CategoryName,
                };

        public static Director MapCreateDirector(CreateDirector createDirector) =>
            createDirector == null
                ? Director.NotFound
                : new()
                {
                    Name = createDirector.Name,
                };

        public static Film MapCreateFilm(CreateFilm createFilm) =>
            createFilm == null
                ? Film.NotFound
                : new()
                {
                    Title = createFilm.Title,
                    Description = createFilm.Description,
                    FormatId = createFilm.FormatId,
                    DirectorId = createFilm.DirectorId,
                    Quantity = createFilm.Quantity,
                    Year = createFilm.Year,
                    Studio = createFilm.Studio,
                    IsFavorite = createFilm.IsFavorite,
                    IsRareCollectibleAndOrValuable = createFilm.IsRareCollectibleAndOrValuable,
                    StarRating = createFilm.StarRating,
                    CreateDate = createFilm.CreateDate,
                };

        public static Format MapCreateFormat(CreateFormat createFormat) =>
            createFormat == null
                ? Format.NotFound
                : new()
                {
                    FormatName = createFormat.FormatName,
                };

        public static Actor MapRenameActor(RenameActor renameActor) =>
            renameActor == null
                ? Actor.NotFound
                : new()
                {
                    ActorId = renameActor.ActorId,
                    Name = renameActor.Name,
                };

        public static Director MapRenameDirector(RenameDirector renameDirector) =>
            renameDirector == null
                ? Director.NotFound
                : new()
                {
                    DirectorId = renameDirector.DirectorId,
                    Name = renameDirector.Name,
                };

        public static Film MapUpdateFilm(UpdateFilm updateFilm) =>
            updateFilm == null
                ? Film.NotFound
                : new()
                {
                    FilmId = updateFilm.FilmId,
                    Title = updateFilm.Title,
                    Description = updateFilm.Description,
                    FormatId = updateFilm.FormatId,
                    DirectorId = updateFilm.DirectorId,
                    Quantity = updateFilm.Quantity,
                    Year = updateFilm.Year,
                    Studio = updateFilm.Studio,
                    StarRating = updateFilm.StarRating,
                    IsFavorite = updateFilm.IsFavorite,
                    IsRareCollectibleAndOrValuable = updateFilm.IsRareCollectibleAndOrValuable,
                    UpdateDate = updateFilm.UpdateDate,
                };
    }
}
