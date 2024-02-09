﻿namespace FilmCatalog.API.Models.DTOs
{
    public class FormatViewDTO
    {
        public required int FormatId { get; init; }

        public required string FormatName { get; init; }

        public static FormatViewDTO NotFound => new()
        {
            FormatId = 0,
            FormatName = "not found",
        };
    }
}
