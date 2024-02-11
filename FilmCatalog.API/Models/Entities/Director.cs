using System;
using System.Collections.Generic;

namespace FilmCatalog.API.Models.Entities;

public partial class Director
{
    public int DirectorId { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Film> Films { get; set; } = new List<Film>();
}
