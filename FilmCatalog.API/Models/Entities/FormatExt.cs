namespace FilmCatalog.API.Models.Entities
{
    public partial class Format
    {
        (bool IsValid, string ErrorMessage) Validate()
        {
            if (string.IsNullOrWhiteSpace(FormatName) || FormatName.Length > 255 || FormatName.Length < 1)
            {
                return (false, "Format name must be between 1 and 255 characters.");
            }

            return (true, string.Empty);
        }
    }
}
