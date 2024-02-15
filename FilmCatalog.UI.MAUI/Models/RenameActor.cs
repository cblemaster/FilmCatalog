namespace FilmCatalog.UI.MAUI.Models
{
    public class RenameActor
    {
        public required int ActorId { get; init; }
        public required string Name { get; init; }

        public (bool IsValid, string ErrorMessage) Validate() =>
            ActorId < 1
                ? (false, "Invalid actor id.")
                : (string.IsNullOrWhiteSpace(Name) || Name.Length > 255 || Name.Length < 1
                    ? (false, "Actor name must be between 1 and 255 characters.")
                    : (true, string.Empty));
    }
}
