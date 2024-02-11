namespace FilmCatalog.API.Models.DTOs
{
    public class DisplayDirector
    {
        public required int DirectorId { get; init; }
        public required string Name { get; init; }
        public static DisplayDirector NotFound => new()
        {
            DirectorId = 0,
            Name = "not found",
        };
    }
}
