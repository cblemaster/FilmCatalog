using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FilmCatalog.UI.MAUI.Models;
using FilmCatalog.UI.MAUI.Services;

namespace FilmCatalog.UI.MAUI.PageModels
{
    public partial class FilmDetailsPageModel(IHttpService httpService) : ObservableObject
    {
        private readonly IHttpService _httpService = httpService;

        [ObservableProperty]
        private DisplayFilm _film = default!;

        [ObservableProperty]
        private bool _canClosePage = true;

        [ObservableProperty]
        private bool _canNavToCreateFilm = true;

        [ObservableProperty]
        private bool _canNavToUpdateFilm = true;

        [ObservableProperty]
        private bool _canDeleteFilm = true;

        [RelayCommand]
        private async Task ClosePageAsync()
        {
            if (!CanClosePage)
            {
                return;
            }

            await Shell.Current.Navigation.PopModalAsync();
        }

        [RelayCommand]
        private async Task NavToCreateFilmAsync()
        {
            if (!CanNavToCreateFilm)
            {
                return;
            }
        }

        [RelayCommand]
        private async Task NavToUpdateFilmAsync()
        {
            if (!CanNavToUpdateFilm)
            {
                return;
            }
        }

        [RelayCommand]
        private async Task DeleteFilmAsync()
        {
            if (!CanDeleteFilm)
            {
                return;
            }
        }
    }
}