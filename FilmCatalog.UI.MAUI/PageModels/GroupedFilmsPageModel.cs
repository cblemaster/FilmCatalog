using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FilmCatalog.UI.MAUI.Models;
using FilmCatalog.UI.MAUI.Pages;
using FilmCatalog.UI.MAUI.Services;
using System.Collections.ObjectModel;

namespace FilmCatalog.UI.MAUI.PageModels
{
    public partial class GroupedFilmsPageModel : ObservableObject
    {
        private readonly ReadOnlyCollection<string> _filterOptions = new(new List<string>() { "Films by actor", "Films by director", "Films by category", "Films by format" });

        private readonly IHttpService _httpService;

        public GroupedFilmsPageModel(IHttpService httpService)
        {
            _httpService = httpService;
            Filters = _filterOptions;

            //Task<ReadOnlyCollection<ReadOnlyCollection<DisplayFilm?>>> a = _httpService.GetFilmsByActorAsync();
        }

        [ObservableProperty]
        private IEnumerable<FilmGroup> _films = default!;

        [ObservableProperty]
        private DisplayFilm _selectedFilm = default!;

        [ObservableProperty]
        private ReadOnlyCollection<string> _filters = default!;

        [ObservableProperty]
        private string _selectedFilter = default!;

        [RelayCommand]
        private void PageAppearing()
        {
            //LoadDataByActorAsync();
            LoadDataByFormatAsync();
            SelectedFilter = "Films by actor";
        }

        [RelayCommand]
        private async Task SelectedFilmChangedAsync() =>
            await Shell.Current.Navigation.PushModalAsync(new FilmDetailsPage(SelectedFilm));

        [RelayCommand]
        private async Task SelectedFilterChangedAsync()
        {
            switch (SelectedFilter)
            {
                case "Films by actor":
                    LoadDataByActorAsync();
                    break;
                case "Films by category":
                    LoadDataByCategoryAsync();
                    break;
                case "Films by director":
                    LoadDataByDirectorAsync();
                    break;
                case "Films by format":
                    LoadDataByFormatAsync();
                    break;
                default:
                    break;
            }
        }

        private async Task LoadDataByActorAsync() => Films = await _httpService.GetFilmsByActorAsync();

        private async Task LoadDataByCategoryAsync() => Films = await _httpService.GetFilmsByCategoryAsync();

        private async Task LoadDataByDirectorAsync() => Films = await _httpService.GetFilmsByDirectorAsync();

        private async Task LoadDataByFormatAsync() => Films = await _httpService.GetFilmsByFormatAsync();
    }
}
