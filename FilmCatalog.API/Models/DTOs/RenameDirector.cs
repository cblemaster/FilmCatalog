namespace FilmCatalog.API.Models.DTOs
{
    public class RenameDirector
    {
        public required int DirectorId { get; init; }
        public required string Name { get; init; }

        public (bool IsValid, string ErrorMessage) Validate() =>
            DirectorId < 1
                ? (false, "Invalid director id.")
                : (string.IsNullOrWhiteSpace(Name) || Name.Length > 255 || Name.Length < 1
                    ? (false, "Director name must be between 1 and 255 characters.")
                    : (true, string.Empty));
    }
}
