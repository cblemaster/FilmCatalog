using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FilmCatalog.UI.MAUI.Models;
using FilmCatalog.UI.MAUI.Pages;
using FilmCatalog.UI.MAUI.Services;

namespace FilmCatalog.UI.MAUI.PageModels
{
    public partial class CreateFilmPageModel : ObservableObject
    {
        private readonly IHttpService _httpService;

        public CreateFilmPageModel(IHttpService httpService)
        {
            _httpService = httpService;
            LoadDirectorsAsync();
            LoadFormatsAsync();
        }

        [ObservableProperty]
        private CreateFilm _createFilm = new();

        [ObservableProperty]
        private IReadOnlyCollection<DisplayDirector> _directors = default!;

        [ObservableProperty]
        private IReadOnlyCollection<DisplayFormat> _formats = default!;

        [ObservableProperty]
        private DisplayDirector? _selectedDirector;

        [ObservableProperty]
        private DisplayFormat _selectedFormat = default!;

        [ObservableProperty]
        private bool _canClosePage = true;

        [ObservableProperty]
        private bool _canCreateActor = true;

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
        private async Task CreateFilmAsync()
        {
            CreateFilm.CreateDate = DateTime.Now;
            CreateFilm.DirectorId = SelectedDirector?.DirectorId ?? null;
            CreateFilm.FormatId = SelectedFormat.FormatId;
            
            if (!CanCreateActor || !CreateFilm.Validate().IsValid)
            {
                return;
            }

            if (await _httpService.CreateFilmAsync(CreateFilm) is DisplayFilm newFilm)
            {
                await Shell.Current.GoToAsync("//Films");
            }
            else
            {
                await Shell.Current.DisplayAlert("Error!", "Error creating film.", "OK");
            }
        }

        private async Task LoadDirectorsAsync()
        {
            var directors = (await _httpService.GetDirectorsAsync()).OrderBy(f => f.Name).ToList();
            directors.Insert(0, new() { DirectorId = 0, Name = "none" });

            Directors = directors.AsReadOnly();
        }

        private async Task LoadFormatsAsync() => Formats = (await _httpService.GetFormatsAsync()).OrderBy(f => f.FormatName).ToList().AsReadOnly();
    }
}
