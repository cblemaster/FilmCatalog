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
            if (!CanDeleteFilm || Film is null || Film.FilmId < 1)
            {
                return;
            }

            bool deleteConfirmed = await Shell.Current.DisplayAlert("Confirm delete", $"Are you sure you want to delete {Film.Title}?", "Yes", "No");

            if (deleteConfirmed)
            {
                if (Film.Actors.Any())
                {
                    await Shell.Current.DisplayAlert("Error!", "Unable to delete film because it is associated with one or more actors.", "OK");
                    return;
                }

                if (Film.Categories.Any())
                {
                    await Shell.Current.DisplayAlert("Error!", "Unable to delete film because it is associated with one or more categories.", "OK");
                    return;
                }

                await _httpService.DeleteFilmAsync(Film.FilmId);
                await Shell.Current.Navigation.PopModalAsync();
            }
        }
    }
}