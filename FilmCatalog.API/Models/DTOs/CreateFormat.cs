namespace FilmCatalog.API.Models.DTOs
{
    public class CreateFormat
    {
        public required string FormatName { get; init; }

        (bool IsValid, string ErrorMessage) Validate() =>
            string.IsNullOrWhiteSpace(FormatName) || FormatName.Length > 255 || FormatName.Length < 1
                ? (false, "Format name must be between 1 and 255 characters.")
                : (true, string.Empty);
    }
}
