namespace FilmCatalog.API.Models.Entities
{
    public partial class Director
    {
        (bool IsValid, string ErrorMessage) Validate() =>
            string.IsNullOrWhiteSpace(Name) || Name.Length > 255 || Name.Length < 1
                ? (false, "Director name must be between 1 and 255 characters.")
                : (true, string.Empty);

        public static Director NotFound => new()
        {
            DirectorId = 0,
            Name = "not found",
            Films = [],
        };
    }
}
