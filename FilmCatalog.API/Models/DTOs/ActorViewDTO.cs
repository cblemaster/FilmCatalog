namespace FilmCatalog.API.Models.DTOs
{
    public class ActorViewDTO
    {
        public required int ActorId { get; init; }
        
        public required string Name { get; init; }

        public required IEnumerable<FilmViewDTO> Films { get; init; }

        public static ActorViewDTO NotFound => new()
        {
            ActorId = 0,
            Name = "not found",
            Films = Enumerable.Empty<FilmViewDTO>(),
        };
    }
}
