namespace FilmCatalog.API.Models.DTOs
{
    public class CreateCategory
    {
        public required string CategoryName { get; init; }

        (bool IsValid, string ErrorMessage) Validate() =>
            string.IsNullOrWhiteSpace(CategoryName) || CategoryName.Length > 255 || CategoryName.Length < 1
                ? (false, "Category name must be between 1 and 255 characters.")
                : (true, string.Empty);
    }
}
