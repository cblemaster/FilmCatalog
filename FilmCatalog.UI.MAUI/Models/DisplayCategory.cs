namespace FilmCatalog.UI.MAUI.Models
{
    public class DisplayCategory
    {
        public required int CategoryId { get; init; }
        public required string CategoryName { get; init; }
        public static DisplayCategory NotFound => new()
        {
            CategoryId = 0,
            CategoryName = "not found",
        };
    }
}
