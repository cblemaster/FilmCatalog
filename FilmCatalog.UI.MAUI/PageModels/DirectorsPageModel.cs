using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FilmCatalog.UI.MAUI.Models;
using FilmCatalog.UI.MAUI.Services;
using System.Collections.ObjectModel;

namespace FilmCatalog.UI.MAUI.PageModels
{
    public partial class DirectorsPageModel(IHttpService httpService) : ObservableObject
    {
        private readonly IHttpService _httpService = httpService;

        [ObservableProperty]
        private ReadOnlyCollection<DisplayDirector> _directors = default!;

        [ObservableProperty]
        private DisplayDirector _selectedDirector = default!;

        [ObservableProperty]
        private string _createDirectorName = string.Empty;

        [ObservableProperty]
        private bool _canCreateDirector = true;

        [ObservableProperty]
        private bool _canRenameDirector = false;

        [ObservableProperty]
        private bool _canDeleteDirector = false;

        [RelayCommand]
        private async Task PageAppearingAsync() => await LoadDataAsync();

        [RelayCommand]
        private void SelectedDirectorChanged()
        {
            CanRenameDirector = true;
            CanDeleteDirector = true;
        }

        [RelayCommand]
        private async Task CreateDirectorAsync()
        {
            if (!CanCreateDirector || string.IsNullOrWhiteSpace(CreateDirectorName))
            {
                return;
            }

            CreateActor createDirector = new() { Name = CreateDirectorName };

            if (await _httpService.CreateActorAsync(createDirector) is DisplayActor)
            {
                await LoadDataAsync();
                CreateDirectorName = string.Empty;
            }
            else
            {
                await Shell.Current.DisplayAlert("Error!", $"Error creating {CreateDirectorName}.", "OK");
            }
        }

        [RelayCommand]
        private async Task RenameDirectorAsync()
        {
            if (!CanRenameDirector) { return; }

            string result = await Shell.Current.DisplayPromptAsync("Rename director", "New name for director:", placeholder: $"{SelectedDirector.Name}", maxLength: 255);

            if (!string.IsNullOrWhiteSpace(result))
            {
                if (result.Length > 0 && result.Length <= 255)
                {
                    RenameDirector renameDirector = new() { DirectorId = SelectedDirector.DirectorId, Name = result };
                    await _httpService.RenameDirectorAsync(SelectedDirector.DirectorId, renameDirector);
                    await LoadDataAsync();
                }
                else
                {
                    await Shell.Current.DisplayAlert("Error!", "Director name must be between one (1) and 255 characters.", "OK");
                }
            }
            else
            {
                await Shell.Current.DisplayAlert("Warning!", "Rename cancelled, or no director name provided.", "OK");
            }
        }

        [RelayCommand]
        private async Task DeleteDirectorAsync()
        {
            if (!CanDeleteDirector || SelectedDirector is null || SelectedDirector.DirectorId < 1)
            {
                return;
            }

            bool deleteConfirmed = await Shell.Current.DisplayAlert("Confirm delete", $"Are you sure you want to delete {SelectedDirector.Name}?", "Yes", "No");

            if (deleteConfirmed)
            {
                await _httpService.DeleteDirectorAsync(SelectedDirector.DirectorId);
                await LoadDataAsync();
            }
        }

        private async Task LoadDataAsync() => Directors = await _httpService.GetDirectorsAsync();
    }
}
