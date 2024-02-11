namespace FilmCatalog.API.Models.Entities
{
    public partial class Category
    {
        (bool IsValid, string ErrorMessage) Validate() =>
            string.IsNullOrWhiteSpace(CategoryName) || CategoryName.Length > 255 || CategoryName.Length < 1
                ? (false, "Category name must be between 1 and 255 characters.")
                : (true, string.Empty);
    }
}
