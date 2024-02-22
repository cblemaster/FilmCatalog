using System.Text;

namespace FilmCatalog.UI.MAUI.Models
{
    public class CreateFilm
    {
        public string Title { get; set; } = default!;
        public string? Description { get; set; }
        public int? DirectorId { get; set; }
        public int FormatId { get; set; }
        public int Quantity { get; set; }
        public string? Year { get; set; }
        public string? Studio { get; set; }
        public int? StarRating { get; set; }
        public bool IsFavorite { get; set; }
        public bool IsRareCollectibleAndOrValuable { get; set; }
        public DateTime CreateDate { get; set; }

        public (bool IsValid, string ErrorMessage) Validate()
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
            if (FormatId < 1)
            {
                AppendToStringBuilder("Invalid format id for film.");
            }
            if (DirectorId is not null && DirectorId < 1)
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
