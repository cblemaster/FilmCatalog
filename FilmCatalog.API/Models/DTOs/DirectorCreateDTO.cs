namespace FilmCatalog.API.Models.DTOs
{
    public class DirectorCreateDTO
    {
        public required string Name { get; init; }

        public (bool IsValid, string ErrorMessage) Validate()
        {
            if (Name.Length > 255 || Name.Length < 1)
            {
                return (false, "Director name must be between 1 and 255 characters.");
            }

            return (true, string.Empty);
        }
    }
}
