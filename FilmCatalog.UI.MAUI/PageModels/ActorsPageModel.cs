using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FilmCatalog.UI.MAUI.Models;
using FilmCatalog.UI.MAUI.Services;
using System.Collections.ObjectModel;

namespace FilmCatalog.UI.MAUI.PageModels
{
    public partial class ActorsPageModel(IHttpService httpService) : ObservableObject
    {
        private readonly IHttpService _httpService = httpService;

        [ObservableProperty]
        private ReadOnlyCollection<DisplayActor> _actors = default!;

        [ObservableProperty]
        private DisplayActor _selectedActor = default!;

        [ObservableProperty]
        private string _createActorName = string.Empty;

        [ObservableProperty]
        private bool _canCreateActor = true;

        [ObservableProperty]
        private bool _canRenameActor = false;

        [ObservableProperty]
        private bool _canDeleteActor = false;

        [RelayCommand]
        private async Task PageAppearingAsync() => await LoadDataAsync();

        [RelayCommand]
        private void SelectedActorChanged()
        {
            CanRenameActor = true;
            CanDeleteActor = true;
        }

        [RelayCommand]
        private async Task CreateActorAsync()
        {
            if (!CanCreateActor || string.IsNullOrWhiteSpace(CreateActorName))
            {
                return;
            }
            
            CreateActor createActor = new() { Name = CreateActorName };
            
            if (await _httpService.CreateActorAsync(createActor) is DisplayActor)
            {
                await LoadDataAsync();
                CreateActorName = string.Empty;
            }
            else
            {
                await Shell.Current.DisplayAlert("Error!", $"Error creating { CreateActorName }.", "OK");
            }
        }

        [RelayCommand]
        private async Task RenameActorAsync()
        {
            if (!CanRenameActor) { return; }

            string result = await Shell.Current.DisplayPromptAsync("Rename actor", "New name for actor:", placeholder: $"{ SelectedActor.Name }", maxLength: 255);

            if (!string.IsNullOrWhiteSpace(result))
            {
                if (result.Length > 0 && result.Length <= 255)
                {
                    RenameActor renameActor = new() { ActorId = SelectedActor.ActorId, Name = result };
                    await _httpService.RenameActorAsync(SelectedActor.ActorId, renameActor);
                    await LoadDataAsync();
                }
                else
                {
                    await Shell.Current.DisplayAlert("Error!", "Actor name must be between one (1) and 255 characters.", "OK");
                }
            }
            else
            {
                await Shell.Current.DisplayAlert("Warning!", "Rename cancelled, or no actor name provided.", "OK");
            }
        }

        [RelayCommand]
        private async Task DeleteActorAsync()
        {
            if (!CanDeleteActor || SelectedActor is null || SelectedActor.ActorId < 1)
            {
                return;
            }

            bool deleteConfirmed = await Shell.Current.DisplayAlert("Confirm delete", $"Are you sure you want to delete {SelectedActor.Name}?", "Yes", "No");

            if (deleteConfirmed)
            {
                await _httpService.DeleteActorAsync(SelectedActor.ActorId);
                await LoadDataAsync();
            }
        }

        private async Task LoadDataAsync() => Actors = await _httpService.GetActorsAsync();
    }
}
