namespace FilmCatalog.API.Models.DTOs
{
    public class CategoryViewWithFilmsDTO
    {
        public required int CategoryId { get; init; }

        public required string CategoryName { get; init; }

        public required IEnumerable<FilmViewDTO> Films { get; init; }

        public static CategoryViewWithFilmsDTO NotFound => new()
        {
            CategoryId = 0,
            CategoryName = "not found",
            Films = Enumerable.Empty<FilmViewDTO>(),
        };
    }
}
