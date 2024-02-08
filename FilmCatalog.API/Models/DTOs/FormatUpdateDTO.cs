namespace FilmCatalog.API.Models.DTOs
{
    public class FormatUpdateDTO
    {
        public required int FormatId { get; init; }

        public required string FormatName { get; init; }

        public required IEnumerable<FilmViewDTO> Films { get; init; }

        public (bool IsValid, string ErrorMessage) Validate()
        {
            if (FormatName.Length > 255 || FormatName.Length < 1)
            {
                return (false, "Format name must be between 1 and 255 characters.");
            }
            else if (FormatId < 1)
            {
                return (false, "Format id must be 1 or greater.");
            }

            return (true, string.Empty);
        }
    }
}
