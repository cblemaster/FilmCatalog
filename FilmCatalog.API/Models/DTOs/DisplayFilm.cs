namespace FilmCatalog.API.Models.DTOs
{
    public class DisplayFilm
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
        public required DateTime CreateDate { get; init; }
        public DateTime? UpdateDate { get; init; }
        public DisplayDirector? Director { get; init; }
        public required DisplayFormat Format { get; init; }
        public required IEnumerable<DisplayActor> Actors { get; init; }
        public required IEnumerable<DisplayCategory> Categories { get; init; }

        public static DisplayFilm NotFound => new()
        {
            FilmId = 0,
            Title = "not found",
            Description = null,
            DirectorId = null,
            FormatId = 0,
            Quantity = 0,
            Year = null,
            Studio = null,
            StarRating = null,
            IsFavorite = false,
            IsRareCollectibleAndOrValuable = false,
            CreateDate = DateTime.MinValue,
            UpdateDate = null,
            Director = null,
            Format = null!,
            Categories = [],
            Actors = [],
        };
    }
}
