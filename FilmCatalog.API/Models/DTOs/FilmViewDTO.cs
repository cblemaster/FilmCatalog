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
        public DirectorViewForFilmDTO? Director { get; init; }
        public required FormatViewForFilmDTO Format { get; init; }
        public required IEnumerable<ActorViewForFilmDTO> Actors { get; init; }
        public required IEnumerable<CategoryViewForFilmDTO> Categories { get; init; }

        public static FilmViewDTO NotFound => new()
        {
            FilmId = 0,
            Title = "not found",
            FormatId = 0,
            Quantity = 0,
            Format = FormatViewForFilmDTO.NotFound,
            Actors = Enumerable.Empty<ActorViewForFilmDTO>(),
            Categories = Enumerable.Empty<CategoryViewForFilmDTO>(),
        };
    }
}
