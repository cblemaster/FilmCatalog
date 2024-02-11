namespace FilmCatalog.API.Models.DTOs
{
    public class RenameActor
    {
        public required int ActorId { get; init; }
        public required string Name { get; init; }
    }
}
