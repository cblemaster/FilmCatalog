using System;
using System.Collections.Generic;

namespace FilmCatalog.API.Models.Entities;

public partial class Film
{
    public int FilmId { get; set; }

    public string Title { get; set; } = null!;

    public string? Description { get; set; }

    public int? DirectorId { get; set; }

    public int FormatId { get; set; }

    public int Quantity { get; set; }

    public string? Year { get; set; }

    public string? Studio { get; set; }

    public int? StarRating { get; set; }

    public bool IsFavorite { get; set; }

    public bool IsRareCollectibleAndOrValuable { get; set; }

    public DateTime CreateDate { get; set; }

    public DateTime? UpdateDate { get; set; }

    public virtual Director? Director { get; set; }

    public virtual Format Format { get; set; } = null!;

    public virtual ICollection<Actor> Actors { get; set; } = new List<Actor>();

    public virtual ICollection<Category> Categories { get; set; } = new List<Category>();
}
