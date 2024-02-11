namespace FilmCatalog.API.Models.Entities;

public partial class Category
{
    public int CategoryId { get; set; }

    public string CategoryName { get; set; } = null!;

    public virtual ICollection<Film> Films { get; set; } = new List<Film>();
}
