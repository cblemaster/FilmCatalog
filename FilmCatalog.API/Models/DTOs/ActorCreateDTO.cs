namespace FilmCatalog.API.Models.DTOs
{
    public class ActorCreateDTO
    {
        public required string Name { get; init; }

        public (bool IsValid, string ErrorMessage) Validate() =>
            Name.Length > 255 || Name.Length < 1
                ? (false, "Actor name must be between 1 and 255 characters.")
                : (true, string.Empty);
    }
}
