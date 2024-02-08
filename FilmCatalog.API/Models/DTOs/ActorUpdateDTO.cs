namespace FilmCatalog.API.Models.DTOs
{
    public class ActorUpdateDTO
    {
        public required int ActorId { get; init; }
        
        public required string Name { get; init; }

        public required IEnumerable<FilmViewDTO> Films { get; init; }

        public (bool IsValid, string ErrorMessage) Validate()
        {
            if (Name.Length > 255 || Name.Length < 1)
            {
                return (false, "Actor name must be between 1 and 255 characters.");
            }
            else if (ActorId < 1)
            {
                return (false, "Actor id must be 1 or greater.");
            }

            return (true, string.Empty);
        }
    }
}
