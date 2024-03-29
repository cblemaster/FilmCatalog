﻿using System.Text;

namespace FilmCatalog.UI.MAUI.Models
{
    public class UpdateFilm
    {
        public required int FilmId { get; init; }
        public required string Title { get; init; }
        public string? Description { get; init; }
        public int? DirectorId { get; init; }
        public required int FormatId { get; init; }
        public required int Quantity { get; init; }
        public string? Year { get; init; }
        public string? Studio { get; init; }
        public int? StarRating { get; init; }
        public required bool IsFavorite { get; init; }
        public required bool IsRareCollectibleAndOrValuable { get; init; }
        public DateTime UpdateDate { get; init; }

        public (bool IsValid, string ErrorMessage) Validate()
        {
            StringBuilder sb = new();

            if (FilmId < 1)
            {
                AppendToStringBuilder("Invalid film id.");
            }

            if (string.IsNullOrWhiteSpace(Title) || Title.Length > 255 || Title.Length < 1)
            {
                AppendToStringBuilder("Film title must be between 1 and 255 characters.");
            }
            if (!string.IsNullOrWhiteSpace(Description) && Description.Length > 255)
            {
                AppendToStringBuilder("Max length for film description is 255 characters.");
            }
            if (FormatId < 1)
            {
                AppendToStringBuilder("Invalid format id for film.");
            }
            if (DirectorId < 1)
            {
                AppendToStringBuilder("Invalid director id for film.");
            }
            if (!string.IsNullOrWhiteSpace(Studio) && Studio.Length > 255)
            {
                AppendToStringBuilder("Max length for film studio is 255 characters.");
            }
            if (!string.IsNullOrWhiteSpace(Year) && Year.Length != 4)
            {
                AppendToStringBuilder("If you provide a year for a film, it must be 4 characters.");
            }
            if (StarRating.HasValue && (StarRating.Value > 5 || StarRating.Value < 0))
            {
                AppendToStringBuilder("If you provide a star rating for a film, it must be between zero and five.");
            }

            return (sb.Length == 0, sb.ToString());

            void AppendToStringBuilder(string error)
            {
                if (sb.Length > 0) { sb.Append("; "); }
                sb.Append(error);
            }
        }
    }
}
