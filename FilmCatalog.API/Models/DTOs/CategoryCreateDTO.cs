namespace FilmCatalog.API.Models.DTOs
{
    public class CategoryCreateDTO
    {
        public required string CategoryName { get; init; }

        public (bool IsValid, string ErrorMessage) Validate()
        {
            if (CategoryName.Length > 255 || CategoryName.Length < 1)
            {
                return (false, "Category name must be between 1 and 255 characters.");
            }

            return (true, string.Empty);
        }
    }
}
