namespace FilmCatalog.API.Models.Entities
{
    public partial class Director
    {
        (bool IsValid, string ErrorMessage) Validate()
        {
            if (string.IsNullOrWhiteSpace(Name) || Name.Length > 255 || Name.Length < 1)
            {
                return (false, "Director name must be between 1 and 255 characters.");
            }

            return (true, string.Empty);
        }
    }
}
