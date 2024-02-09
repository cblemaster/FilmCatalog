namespace FilmCatalog.API.Models.DTOs
{
    public class ActorViewWithFilmsDTO
    {
        public required int ActorId { get; init; }

        public required string Name { get; init; }

        public required IEnumerable<FilmViewDTO> Films { get; init; }

        public static ActorViewWithFilmsDTO NotFound => new()
        {
            ActorId = 0,
            Name = "not found",
            Films = Enumerable.Empty<FilmViewDTO>(),
        };
    }
}
