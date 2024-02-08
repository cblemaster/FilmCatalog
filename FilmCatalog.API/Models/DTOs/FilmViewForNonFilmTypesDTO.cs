namespace FilmCatalog.API.Models.DTOs
{
    public class FilmViewForNonFilmTypesDTO
    {
        public required int FilmId { get; init; }
        public required string Title { get; init; }
        public string? Description { get; init; }
        public required int Quantity { get; init; }
        public string? Year { get; init; }
        public string? Studio { get; init; }
        public bool IsFavorite { get; init; }
        public bool IsRareCollectibleAndOrValuable { get; init; }
        public DateTime CreateDate { get; init; }
        public DateTime? UpdateDate { get; init; }

        public static FilmViewForNonFilmTypesDTO NotFound => new()
        {
            FilmId = 0,
            Title = "not found",
            Quantity = 0,
        };
    }
}
