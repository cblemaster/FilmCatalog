using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FilmCatalog.UI.MAUI.Models;
using FilmCatalog.UI.MAUI.Services;
using System.Collections.ObjectModel;

namespace FilmCatalog.UI.MAUI.PageModels
{
    public partial class FormatsPageModel(IHttpService httpService) : ObservableObject
    {
        private readonly IHttpService _httpService = httpService;

        [ObservableProperty]
        private ReadOnlyCollection<DisplayFormat> _formats = default!;

        [ObservableProperty]
        private DisplayFormat _selectedFormat = default!;

        [ObservableProperty]
        private string _createFormatName = string.Empty;

        [ObservableProperty]
        private bool _canCreateFormat = true;

        [ObservableProperty]
        private bool _canDeleteFormat = false;

        [RelayCommand]
        private async Task PageAppearingAsync() => await LoadDataAsync();

        [RelayCommand]
        private void SelectedFormatChanged()
        {
            CanDeleteFormat = true;
        }

        [RelayCommand]
        private async Task CreateFormatAsync()
        {
            if (!CanCreateFormat || string.IsNullOrWhiteSpace(CreateFormatName))
            {
                return;
            }

            CreateFormat createFormat = new() { FormatName = CreateFormatName };

            if (await _httpService.CreateFormatAsync(createFormat) is DisplayFormat)
            {
                await LoadDataAsync();
                CreateFormatName = string.Empty;
            }
            else
            {
                await Shell.Current.DisplayAlert("Error!", $"Error creating {CreateFormatName}.", "OK");
            }
        }

        [RelayCommand]
        private async Task DeleteFormatAsync()
        {
            if (!CanDeleteFormat || SelectedFormat is null || SelectedFormat.FormatId < 1)
            {
                return;
            }

            bool deleteConfirmed = await Shell.Current.DisplayAlert("Confirm delete", $"Are you sure you want to delete {SelectedFormat.FormatName}?", "Yes", "No");

            if (deleteConfirmed)
            {
                await _httpService.DeleteFormatAsync(SelectedFormat.FormatId);
                await LoadDataAsync();
            }
        }

        private async Task LoadDataAsync() => Formats = await _httpService.GetFormatsAsync();
    }
}
