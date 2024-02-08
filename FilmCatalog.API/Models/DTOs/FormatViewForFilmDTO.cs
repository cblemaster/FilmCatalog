namespace FilmCatalog.API.Models.DTOs
{
    public class FormatViewForFilmDTO
    {
        public required int FormatId { get; init; }

        public required string FormatName { get; init; }

        public static FormatViewForFilmDTO NotFound => new()
        {
            FormatId = 0,
            FormatName = "not found",
        };
    }
}
