namespace FilmCatalog.API.Models.DTOs
{
    public class UpdateFilm
    {
        public required int FilmId { get; init; }
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
        public DateTime UpdateDate { get; init; } = DateTime.Now;
    }
}
