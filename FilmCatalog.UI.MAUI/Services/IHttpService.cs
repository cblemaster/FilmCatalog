using FilmCatalog.UI.MAUI.Models;
using System.Collections.ObjectModel;

namespace FilmCatalog.UI.MAUI.Services
{
    public interface IHttpService
    {
        void AssignActorsToFilm(int filmId, ICollection<DisplayActor> actors);
        void AssignCategoriesToFilm(int filmId, ICollection<DisplayCategory> categories);
        Task<DisplayActor?> CreateActor(CreateActor actor);
        Task<DisplayCategory?> CreateCategory(CreateCategory category);
        Task<DisplayDirector?> CreateDirector(CreateDirector director);
        Task<DisplayFilm?> CreateFilm(CreateFilm film);
        Task<DisplayFormat?> CreateFormat(CreateFormat format);
        void DeleteActor(int actorId);
        void DeleteCategory(int categoryId);
        void DeleteDirector(int directorId);
        void DeleteFilm(int filmId);
        void DeleteFormat(int formatId);
        Task<DisplayActor> GetActor(int actorId);
        Task<ReadOnlyCollection<DisplayActor?>> GetActors();
        Task<DisplayCategory> GetCategory(int categoryId);
        Task<ReadOnlyCollection<DisplayCategory?>> GetCategories();
        Task<DisplayDirector> GetDirector(int directorId);
        Task<ReadOnlyCollection<DisplayDirector?>> GetDirectors();
        Task<DisplayFilm> GetFilm(int filmId);
        Task<ReadOnlyCollection<DisplayFilm?>> GetFilms();
        Task<ReadOnlyCollection<DisplayFilm?>> GetFavoriteFilms();
        Task<ReadOnlyCollection<DisplayFilm?>> GetRareFilms();
        Task<ReadOnlyCollection<DisplayFilm?>> GetFivestarFilms();
        Task<ReadOnlyCollection<ReadOnlyCollection<DisplayFilm?>>> GetFilmsByActor();
        Task<ReadOnlyCollection<ReadOnlyCollection<DisplayFilm?>>> GetFilmsByCategory();
        Task<ReadOnlyCollection<ReadOnlyCollection<DisplayFilm?>>> GetFilmsByDirector();
        Task<ReadOnlyCollection<ReadOnlyCollection<DisplayFilm?>>> GetFilmsByFormat();
        Task<DisplayFormat> GetFormat(int formatId);
        Task<ReadOnlyCollection<DisplayFormat?>> GetFormats();
        void RemoveActorsFromFilm(int filmId, ICollection<DisplayActor> actors);
        void RemoveCategoriesFromFilm(int filmId, ICollection<DisplayCategory> categories);
        void RenameActor(int actorId, RenameActor actor);
        void RenameDirector(int directorId, RenameDirector director);
        void UpdateFilm(int filmId, UpdateFilm film);
    }
}
