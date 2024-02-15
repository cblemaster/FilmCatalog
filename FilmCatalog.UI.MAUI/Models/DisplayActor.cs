namespace FilmCatalog.UI.MAUI.Models
{
    public class DisplayActor
    {
        public required int ActorId { get; init; }
        public required string Name { get; init; }

        public static DisplayActor NotFound => new()
        {
            ActorId = 0,
            Name = "not found",
        };
    }
}
