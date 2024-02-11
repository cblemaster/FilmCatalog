namespace FilmCatalog.API.Models.Entities
{
    public partial class Format
    {
        (bool IsValid, string ErrorMessage) Validate() =>
            string.IsNullOrWhiteSpace(FormatName) || FormatName.Length > 255 || FormatName.Length < 1
                ? (false, "Format name must be between 1 and 255 characters.")
                : (true, string.Empty);

        public static Format NotFound => new()
        {
            FormatId = 0,
            FormatName = "not found",
            Films = [],
        };
    }
}
