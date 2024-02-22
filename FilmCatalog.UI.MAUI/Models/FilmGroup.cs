namespace FilmCatalog.UI.MAUI.Models
{
    public class FilmGroup(string name, IList<DisplayFilm> films) : List<DisplayFilm>(films)
    {
        public string Name { get; init; } = name;
    }
}