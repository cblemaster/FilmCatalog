using System;
using System.Collections.Generic;

namespace FilmCatalog.API.Models.Entities;

public partial class Format
{
    public int FormatId { get; set; }

    public string FormatName { get; set; } = null!;

    public virtual ICollection<Film> Films { get; set; } = new List<Film>();
}
