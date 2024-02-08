﻿namespace FilmCatalog.API.Models.DTOs
{
    public class CategoryViewDTO
    {
        public required int CategoryId { get; init; }

        public required string CategoryName { get; init; }

        public required IEnumerable<FilmViewForNonFilmTypesDTO> Films { get; init; }

        public static CategoryViewDTO NotFound => new()
        {
            CategoryId = 0,
            CategoryName = "not found",
            Films = Enumerable.Empty<FilmViewForNonFilmTypesDTO>(),
        };
    }
}
