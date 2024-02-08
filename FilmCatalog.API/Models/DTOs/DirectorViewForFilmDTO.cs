namespace FilmCatalog.API.Models.DTOs
{
    public class DirectorViewForFilmDTO
    {
        public required int DirectorId { get; init; }

        public required string Name { get; init; }

        public static DirectorViewForFilmDTO NotFound => new()
        {
            DirectorId = 0,
            Name = "not found",
        };
    }
}
