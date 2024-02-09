namespace FilmCatalog.API.Models.DTOs
{
    public class FormatViewWithFilmsDTO
    {
        public required int FormatId { get; init; }

        public required string FormatName { get; init; }

        public required IEnumerable<FilmViewDTO> Films { get; init; }

        public static FormatViewWithFilmsDTO NotFound => new()
        {
            FormatId = 0,
            FormatName = "not found",
            Films = Enumerable.Empty<FilmViewDTO>(),
        };
    }
}
