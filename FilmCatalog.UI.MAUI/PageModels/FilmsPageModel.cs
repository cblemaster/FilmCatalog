using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FilmCatalog.UI.MAUI.Models;
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

        [ObservableProperty]
        private string _createFilmName = string.Empty;

        [ObservableProperty]
        private bool _canCreateFilm = true;

        [ObservableProperty]
        private bool _canUpdateFilm = false;

        [ObservableProperty]
        private bool _canDeleteFilm = false;

        [RelayCommand]
        private async Task PageAppearingAsync() => await LoadDataAsync();

        [RelayCommand]
        private void SelectedFilmChanged()
        {
            CanUpdateFilm = true;
            CanDeleteFilm = true;
        }

        [RelayCommand]
        private async Task CreateFilmAsync()
        {
            //if (!CanCreateFilm || string.IsNullOrWhiteSpace(CreateFilmName))
            //{
            //    return;
            //}

            //CreateFilm createFilm = new() { Name = CreateFilmName };

            //if (await _httpService.CreateFilmAsync(createFilm) is DisplayFilm)
            //{
            //    await LoadDataAsync();
            //    CreateFilmName = string.Empty;
            //}
            //else
            //{
            //    await Shell.Current.DisplayAlert("Error!", $"Error creating {CreateFilmName}.", "OK");
            //}
        }

        [RelayCommand]
        private async Task UpdateFilmAsync()
        {
            //if (!CanUpdateFilm) { return; }

            //string result = await Shell.Current.DisplayPromptAsync("Update film", "New name for actor:", placeholder: $"{SelectedFilm.Title}", maxLength: 255);

            //if (!string.IsNullOrWhiteSpace(result))
            //{
            //    if (result.Length > 0 && result.Length <= 255)
            //    {
            //        UpdateFilm updateFilm = new() { ActorId = SelectedActor.ActorId, Name = result };
            //        await _httpService.UpdateFilmAsync(SelectedFilm.FilmId, updateFilm);
            //        await LoadDataAsync();
            //    }
            //    else
            //    {
            //        await Shell.Current.DisplayAlert("Error!", "Actor name must be between one (1) and 255 characters.", "OK");
            //    }
            //}
            //else
            //{
            //    await Shell.Current.DisplayAlert("Warning!", "Rename cancelled, or no actor name provided.", "OK");
            //}
        }

        [RelayCommand]
        private async Task DeleteActorAsync()
        {
            //if (!CanDeleteActor || SelectedActor is null || SelectedActor.ActorId < 1)
            //{
            //    return;
            //}

            //bool deleteConfirmed = await Shell.Current.DisplayAlert("Confirm delete", $"Are you sure you want to delete {SelectedActor.Name}?", "Yes", "No");

            //if (deleteConfirmed)
            //{
            //    await _httpService.DeleteActorAsync(SelectedActor.ActorId);
            //    await LoadDataAsync();
            //}
        }

        private async Task LoadDataAsync() => Films = await _httpService.GetFilmsAsync();
    }
}
