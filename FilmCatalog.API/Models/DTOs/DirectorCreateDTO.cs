﻿namespace FilmCatalog.API.Models.DTOs
{
    public class DirectorCreateDTO
    {
        public required string Name { get; init; }

        public (bool IsValid, string ErrorMessage) Validate() =>
            Name.Length > 255 || Name.Length < 1
                ? (false, "Director name must be between 1 and 255 characters.")
                : (true, string.Empty);
    }
}
