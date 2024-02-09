﻿namespace FilmCatalog.API.Models.DTOs
{
    public class DirectorViewDTO
    {
        public required int DirectorId { get; init; }

        public required string Name { get; init; }

        public static DirectorViewDTO NotFound => new()
        {
            DirectorId = 0,
            Name = "not found",
        };
    }
}
