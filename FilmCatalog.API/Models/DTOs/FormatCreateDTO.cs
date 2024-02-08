namespace FilmCatalog.API.Models.DTOs
{
    public class FormatCreateDTO
    {
        public required string FormatName { get; init; }

        public (bool IsValid, string ErrorMessage) Validate()
        {
            if (FormatName.Length > 255 || FormatName.Length < 1)
            {
                return (false, "Format name must be between 1 and 255 characters.");
            }

            return (true, string.Empty);
        }
    }
}
