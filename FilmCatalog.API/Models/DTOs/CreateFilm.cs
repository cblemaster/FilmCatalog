namespace FilmCatalog.API.Models.DTOs
{
    public class CreateFilm
    {
        public required string Title { get; init; }
        public string? Description { get; init; }
        public int? DirectorId { get; init; }
        public required int FormatId { get; init; }
        public required int Quantity { get; init; }
        public string? Year { get; init; }
        public string? Studio { get; init; }
        public int? StarRating { get; init; }
        public required bool IsFavorite { get; init; }
        public required bool IsRareCollectibleAndOrValuable { get; init; }
        public required DateTime CreateDate { get; init; } = DateTime.Now;
    }
}
