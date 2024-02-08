namespace FilmCatalog.API.Models.DTOs
{
    public class DirectorUpdateDTO
    {
        public required int DirectorId { get; init; }

        public required string Name { get; init; }

        public required IEnumerable<FilmViewDTO> Films { get; init; }

        public (bool IsValid, string ErrorMessage) Validate()
        {
            if (Name.Length > 255 || Name.Length < 1)
            {
                return (false, "Director name must be between 1 and 255 characters.");
            }
            else if (DirectorId < 1)
            {
                return (false, "Director id must be 1 or greater.");
            }

            return (true, string.Empty);
        }
    }
}
