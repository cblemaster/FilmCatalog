namespace FilmCatalog.API.Models.DTOs
{
    public class CreateDirector
    {
        public required string Name { get; init; }

        public (bool IsValid, string ErrorMessage) Validate() =>
            string.IsNullOrWhiteSpace(Name) || Name.Length > 255 || Name.Length < 1
                ? (false, "Director name must be between 1 and 255 characters.")
                : (true, string.Empty);
    }
}
