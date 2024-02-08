namespace FilmCatalog.API.Models.DTOs
{
    public class CategoryUpdateDTO
    {
        public required int CategoryId { get; init; }

        public required string CategoryName { get; init; }

        public required IEnumerable<FilmViewDTO> Films { get; init; }

        public (bool IsValid, string ErrorMessage) Validate()
        {
            if (CategoryName.Length > 255 || CategoryName.Length < 1)
            {
                return (false, "Category name must be between 1 and 255 characters.");
            }
            else if (CategoryId < 1)
            {
                return (false, "Category id must be 1 or greater.");
            }

            return (true, string.Empty);
        }
    }
}
