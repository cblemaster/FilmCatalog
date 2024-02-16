using FilmCatalog.UI.MAUI.Models;
using System.Collections.ObjectModel;

namespace FilmCatalog.UI.MAUI.Services
{
    public interface IHttpService
    {
        void AssignActorsToFilmAsync(int filmId, ICollection<DisplayActor> actors);
        void AssignCategoriesToFilmAsync(int filmId, ICollection<DisplayCategory> categories);
        Task<DisplayActor?> CreateActorAsync(CreateActor actor);
        Task<DisplayCategory?> CreateCategoryAsync(CreateCategory category);
        Task<DisplayDirector?> CreateDirectorAsync(CreateDirector director);
        Task<DisplayFilm?> CreateFilmAsync(CreateFilm film);
        Task<DisplayFormat?> CreateFormatAsync(CreateFormat format);
        void DeleteActorAsync(int actorId);
        void DeleteCategoryAsync(int categoryId);
        void DeleteDirectorAsync(int directorId);
        void DeleteFilmAsync(int filmId);
        void DeleteFormatAsync(int formatId);
        Task<DisplayActor> GetActorAsync(int actorId);
        Task<ReadOnlyCollection<DisplayActor?>> GetActorsAsync();
        Task<DisplayCategory> GetCategoryAsync(int categoryId);
        Task<ReadOnlyCollection<DisplayCategory?>> GetCategoriesAsync();
        Task<DisplayDirector> GetDirectorAsync(int directorId);
        Task<ReadOnlyCollection<DisplayDirector?>> GetDirectorsAsync();
        Task<DisplayFilm> GetFilmAsync(int filmId);
        Task<ReadOnlyCollection<DisplayFilm?>> GetFilmsAsync();
        Task<ReadOnlyCollection<DisplayFilm?>> GetFavoriteFilmsAsync();
        Task<ReadOnlyCollection<DisplayFilm?>> GetRareFilmsAsync();
        Task<ReadOnlyCollection<DisplayFilm?>> GetFivestarFilmsAsync();
        Task<ReadOnlyCollection<ReadOnlyCollection<DisplayFilm?>>> GetFilmsByActorAsync();
        Task<ReadOnlyCollection<ReadOnlyCollection<DisplayFilm?>>> GetFilmsByCategoryAsync();
        Task<ReadOnlyCollection<ReadOnlyCollection<DisplayFilm?>>> GetFilmsByDirectorAsync();
        Task<ReadOnlyCollection<ReadOnlyCollection<DisplayFilm?>>> GetFilmsByFormatAsync();
        Task<DisplayFormat> GetFormatAsync(int formatId);
        Task<ReadOnlyCollection<DisplayFormat?>> GetFormatsAsync();
        void RemoveActorsFromFilmAsync(int filmId, ICollection<DisplayActor> actors);
        void RemoveCategoriesFromFilmAsync(int filmId, ICollection<DisplayCategory> categories);
        void RenameActorAsync(int actorId, RenameActor actor);
        void RenameDirectorAsync(int directorId, RenameDirector director);
        void UpdateFilmAsync(int filmId, UpdateFilm film);
    }
}
