using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FilmCatalog.UI.MAUI.Models;
using FilmCatalog.UI.MAUI.Pages;
using FilmCatalog.UI.MAUI.Services;
using System.Collections.ObjectModel;

namespace FilmCatalog.UI.MAUI.PageModels
{
    public partial class FilmsPageModel(IHttpService httpService) : ObservableObject
    {
        private readonly IHttpService _httpService = httpService;

        [ObservableProperty]
        private ReadOnlyCollection<DisplayFilm> _films = default!;

        [ObservableProperty]
        private DisplayFilm _selectedFilm = default!;

        [RelayCommand]
        private void PageAppearing() => LoadDataAsync();

        [RelayCommand]
        private async Task SelectedFilmChanged() =>
            await Shell.Current.Navigation.PushModalAsync(new FilmDetailsPage(SelectedFilm));

        private async Task LoadDataAsync() => Films = await _httpService.GetFilmsAsync();
    }
}
