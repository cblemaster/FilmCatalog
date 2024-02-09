namespace FilmCatalog.API.Models.DTOs
{
    public class FilmViewDTO
    {
        public required int FilmId { get; init; }
        public required string Title { get; init; }
        public string? Description { get; init; }
        public int? DirectorId { get; init; }
        public required int FormatId { get; init; }
        public required int Quantity { get; init; }
        public string? Year { get; init; }
        public string? Studio { get; init; }
        public bool IsFavorite { get; init; }
        public bool IsRareCollectibleAndOrValuable { get; init; }
        public DateTime CreateDate { get; init; }
        public DateTime? UpdateDate { get; init; }
        public DirectorViewDTO? Director { get; init; }
        public required FormatViewDTO Format { get; init; }
        public required IEnumerable<ActorViewDTO> Actors { get; init; }
        public required IEnumerable<CategoryViewDTO> Categories { get; init; }

        public static FilmViewDTO NotFound => new()
        {
            FilmId = 0,
            Title = "not found",
            FormatId = 0,
            Quantity = 0,
            Format = FormatViewDTO.NotFound,
            Actors = Enumerable.Empty<ActorViewDTO>(),
            Categories = Enumerable.Empty<CategoryViewDTO>(),
        };
    }
}
