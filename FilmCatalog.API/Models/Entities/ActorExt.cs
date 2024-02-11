namespace FilmCatalog.API.Models.Entities
{
    public partial class Actor
    {
        (bool IsValid, string ErrorMessage) Validate() =>
            string.IsNullOrWhiteSpace(Name) || Name.Length > 255 || Name.Length < 1
                ? (false, "Actor name must be between 1 and 255 characters.")
                : (true, string.Empty);

        public static Actor NotFound => new()
        {
            ActorId = 0,
            Name = "not found",
            Films = [],
        };
    }
}
