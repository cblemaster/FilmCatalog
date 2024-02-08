namespace FilmCatalog.API.Models.DTOs
{
    public class FormatCreateDTO
    {
        public required string FormatName { get; init; }

        public (bool IsValid, string ErrorMessage) Validate() => FormatName.Length > 255 || FormatName.Length < 1
                ? (false, "Format name must be between 1 and 255 characters.")
                : (true, string.Empty);
    }
}
