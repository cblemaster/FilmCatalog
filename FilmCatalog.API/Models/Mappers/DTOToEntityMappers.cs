﻿using FilmCatalog.API.Models.DTOs;
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
                    CreateDate = DateTime.Now,
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

        public static Film MapUpdateFilm(Film filmToUpdate, UpdateFilm updateFilm)
        {
            if (updateFilm is null)
            {
                return filmToUpdate;
            }

            filmToUpdate.Title = updateFilm.Title;
            filmToUpdate.Description = updateFilm.Description;
            filmToUpdate.FormatId = updateFilm.FormatId;
            filmToUpdate.DirectorId = updateFilm.DirectorId;
            filmToUpdate.Quantity = updateFilm.Quantity;
            filmToUpdate.Year = updateFilm.Year;
            filmToUpdate.Studio = updateFilm.Studio;
            filmToUpdate.StarRating = updateFilm.StarRating;
            filmToUpdate.IsFavorite = updateFilm.IsFavorite;
            filmToUpdate.IsRareCollectibleAndOrValuable = updateFilm.IsRareCollectibleAndOrValuable;
            filmToUpdate.UpdateDate = DateTime.Now;

            return filmToUpdate;

        }
    }
}
