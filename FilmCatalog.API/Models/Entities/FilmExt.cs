using System.Text;

namespace FilmCatalog.API.Models.Entities
{
    public partial class Film
    {
        private bool HasActors => Actors.Count != 0;

        private bool HasCategories => Categories.Count != 0;

        public int ActorCount => HasActors ? Actors.Count : 0;

        public int CategoryCount => HasCategories ? Categories.Count : 0;

        (bool IsValid, string ErrorMessage) Validate()
        {
            StringBuilder sb = new();

            if (string.IsNullOrWhiteSpace(Title) || Title.Length > 255 || Title.Length < 1)
            {
                AppendToStringBuilder("Film title must be between 1 and 255 characters.");
            }
            if (!string.IsNullOrWhiteSpace(Description) && Description.Length > 255)
            {
                AppendToStringBuilder("Max length for film description is 255 characters.");
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

            return (sb.Length > 0, sb.ToString());

            void AppendToStringBuilder(string error)
            {
                if (sb.Length > 0) { sb.Append("; "); }
                sb.Append(error);
            }
        }

        public static Film NotFound => new()
        {
            FilmId = 0,
            Title = "not found",
            Description = null,
            DirectorId = null,
            FormatId = 0,
            Quantity = 0,
            Year = null,
            Studio = null,
            StarRating = null,
            IsFavorite = false,
            IsRareCollectibleAndOrValuable = false,
            CreateDate = DateTime.MinValue,
            UpdateDate = null,
            Director = null,
            Format = null!,
            Categories = [],
            Actors = [],
        };
    }
}
