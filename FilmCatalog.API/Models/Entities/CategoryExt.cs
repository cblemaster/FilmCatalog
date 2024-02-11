namespace FilmCatalog.API.Models.Entities
{
    public partial class Category
    {
        (bool IsValid, string ErrorMessage) Validate()
        {
            if (string.IsNullOrWhiteSpace(CategoryName) || CategoryName.Length > 255 || CategoryName.Length < 1)
            {
                return (false, "Category name must be between 1 and 255 characters.");
            }

            return (true, string.Empty);
        }
    }
}
