namespace FilmCatalog.API.Models.Entities;

public partial class Actor
{
    public int ActorId { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Film> Films { get; set; } = new List<Film>();
}
