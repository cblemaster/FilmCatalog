using FilmCatalog.API.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace FilmCatalog.API.Context
{
    public partial class FilmCatalogContext
    {
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder) =>
            modelBuilder.Entity<Film>(e =>
            {
                e.Ignore(f => f.CategoryCount);
                e.Ignore(f => f.ActorCount);

                e.Navigation<IEnumerable<Actor>>(n => n.Actors).AutoInclude();
                e.Navigation<IEnumerable<Category>>(n => n.Categories).AutoInclude();
                e.Navigation<Director>(n => n.Director).AutoInclude();
                e.Navigation<Format>(n => n.Format).AutoInclude();
            });
    }
}
