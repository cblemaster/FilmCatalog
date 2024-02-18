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
        Task DeleteActorAsync(int actorId);
        Task DeleteCategoryAsync(int categoryId);
        Task DeleteDirectorAsync(int directorId);
        Task DeleteFilmAsync(int filmId);
        Task DeleteFormatAsync(int formatId);
        Task<DisplayActor> GetActorAsync(int actorId);
        Task<ReadOnlyCollection<DisplayActor>> GetActorsAsync();
        Task<DisplayCategory> GetCategoryAsync(int categoryId);
        Task<ReadOnlyCollection<DisplayCategory>> GetCategoriesAsync();
        Task<DisplayDirector> GetDirectorAsync(int directorId);
        Task<ReadOnlyCollection<DisplayDirector>> GetDirectorsAsync();
        Task<DisplayFilm> GetFilmAsync(int filmId);
        Task<ReadOnlyCollection<DisplayFilm>> GetFilmsAsync();
        Task<ReadOnlyCollection<DisplayFilm>> GetFavoriteFilmsAsync();
        Task<ReadOnlyCollection<DisplayFilm>> GetRareFilmsAsync();
        Task<ReadOnlyCollection<DisplayFilm>> GetFivestarFilmsAsync();
        Task<ReadOnlyCollection<ReadOnlyCollection<DisplayFilm?>>> GetFilmsByActorAsync();
        Task<ReadOnlyCollection<ReadOnlyCollection<DisplayFilm?>>> GetFilmsByCategoryAsync();
        Task<ReadOnlyCollection<ReadOnlyCollection<DisplayFilm?>>> GetFilmsByDirectorAsync();
        Task<ReadOnlyCollection<ReadOnlyCollection<DisplayFilm?>>> GetFilmsByFormatAsync();
        Task<DisplayFormat> GetFormatAsync(int formatId);
        Task<ReadOnlyCollection<DisplayFormat>> GetFormatsAsync();
        void RemoveActorsFromFilmAsync(int filmId, ICollection<DisplayActor> actors);
        void RemoveCategoriesFromFilmAsync(int filmId, ICollection<DisplayCategory> categories);
        Task RenameActorAsync(int actorId, RenameActor actor);
        Task RenameDirectorAsync(int directorId, RenameDirector director);
        Task UpdateFilmAsync(int filmId, UpdateFilm film);
    }
}
