using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FilmCatalog.UI.MAUI.Models;
using FilmCatalog.UI.MAUI.Pages;
using FilmCatalog.UI.MAUI.Services;
using System.Collections.ObjectModel;

namespace FilmCatalog.UI.MAUI.PageModels
{
    public partial class FilmsPageModel : ObservableObject
    {
        private readonly ReadOnlyCollection<string> _filterOptions = new(new List<string>() { "All films" ,"Favorites", "Rare, collectible, and/or valuable", "Five star", "Films by actor", "Films by director", "Films by category", "Films by format"});
                
        private readonly IHttpService _httpService;

        public FilmsPageModel(IHttpService httpService)
        {
            _httpService = httpService;
            Filters = _filterOptions;
        }

        [ObservableProperty]
        private ReadOnlyCollection<DisplayFilm> _films = default!;

        [ObservableProperty]
        private DisplayFilm _selectedFilm = default!;

        [ObservableProperty]
        private ReadOnlyCollection<string> _filters = default!;

        [ObservableProperty]
        private string _selectedFilter = default!;

        [RelayCommand]
        private void PageAppearing()
        {
            LoadDataAsync();
            SelectedFilter = "All films";
        }

        [RelayCommand]
        private async Task SelectedFilmChangedAsync() =>
            await Shell.Current.Navigation.PushModalAsync(new FilmDetailsPage(SelectedFilm));

        [RelayCommand]
        private async Task SelectedFilterChangedAsync()
        {
            switch (SelectedFilter)
            {
                case "All films":
                    LoadDataAsync();
                    break;
                case "Favorites":
                    LoadDataFavoritesAsync();
                    break;
                case "Rare, collectible, and/or valuable":
                    LoadDataRareAsync();
                    break;
                case "Five star":
                    LoadDataFivestarAsync();
                    break;
                default:
                    break;
            }
        }

        private async Task LoadDataAsync() => Films = await _httpService.GetFilmsAsync();

        private async Task LoadDataFavoritesAsync() => Films = await _httpService.GetFavoriteFilmsAsync();

        private async Task LoadDataRareAsync() => Films = await _httpService.GetRareFilmsAsync();

        private async Task LoadDataFivestarAsync() => Films = await _httpService.GetFivestarFilmsAsync();
    }
}
