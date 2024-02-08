namespace FilmCatalog.API.Models.DTOs
{
    public class ActorViewForFilmDTO
    {
        public required int ActorId { get; init; }

        public required string Name { get; init; }

        public static ActorViewForFilmDTO NotFound => new()
        {
            ActorId = 0,
            Name = "not found",
        };
    }
}
