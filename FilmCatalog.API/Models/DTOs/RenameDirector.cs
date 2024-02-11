namespace FilmCatalog.API.Models.DTOs
{
    public class RenameDirector
    {
        public required int DirectorId { get; init; }
        public required string Name { get; init; }
    }
}
