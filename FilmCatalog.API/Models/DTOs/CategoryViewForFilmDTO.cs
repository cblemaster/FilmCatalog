namespace FilmCatalog.API.Models.DTOs
{
    public class CategoryViewForFilmDTO
    {
        public required int CategoryId { get; init; }

        public required string CategoryName { get; init; }

        public static CategoryViewForFilmDTO NotFound => new()
        {
            CategoryId = 0,
            CategoryName = "not found",
        };
    }
}
