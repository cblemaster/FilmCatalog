﻿namespace FilmCatalog.UI.MAUI.Models
{
    public class DisplayFormat
    {
        public required int FormatId { get; init; }
        public required string FormatName { get; init; }
        public static DisplayFormat NotFound => new()
        {
            FormatId = 0,
            FormatName = "not found",
        };
    }
}
