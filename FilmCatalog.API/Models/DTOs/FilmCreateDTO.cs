namespace FilmCatalog.API.Models.DTOs
{
    public class FilmCreateDTO
    {
        public required string Title { get; init; }
        public string? Description { get; init; }
        public int? DirectorId { get; init; }
        public required int FormatId { get; init; }
        public required int Quantity { get; init; }
        public string? Year { get; init; }
        public string? Studio {  get; init; }
        public bool IsFavorite { get; init; }
        public bool IsRareCollectibleAndOrValuable { get; init; }
        public DateTime CreateDate { get; init; }
        public DirectorViewDTO? Director {  get; init; }
        public required FormatViewDTO Format {  get; init; }

        public (bool IsValid, string ErrorMessage) Validate()
        {
            if (Title.Length > 255 || Title.Length < 1)
            {
                return (false, "Film title must be between 1 and 255 characters.");
            }
            else if (Description?.Length > 255)
            {
                return (false, "Film description must be fewer than 255 characters.");
            }
            else if (DirectorId.HasValue && DirectorId < 1)
            {
                return (false, "Film director id must be 1 or greater.");
            }
            else if (FormatId < 1)
            {
                return (false, "Film format id must be 1 or greater.");
            }
            else if (Quantity < 1)
            {
                return (false, "Film quantity must be 1 or greater.");
            }
            else if (Year?.Length > 4)
            {
                return (false, "Film year must be fewer than 4 characters.");
            }
            else if (Studio?.Length > 255)
            {
                return (false, "Film studio must be fewer than 255 characters.");
            }
            return (true, string.Empty);
        }
    }
}
