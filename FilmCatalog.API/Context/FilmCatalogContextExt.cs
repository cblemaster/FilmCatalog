using FilmCatalog.API.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace FilmCatalog.API.Context
{
    public partial class FilmCatalogContext
    {
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Film>(e => e.Ignore(f => f.CategoryCount));
            modelBuilder.Entity<Film>(e => e.Ignore(f => f.ActorCount));
        }
    }
}
