using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FilmCatalog.UI.MAUI.Models;
using FilmCatalog.UI.MAUI.Services;
using System.Collections.ObjectModel;

namespace FilmCatalog.UI.MAUI.PageModels
{
    public partial class CategoriesPageModel(IHttpService httpService) : ObservableObject
    {
        private readonly IHttpService _httpService = httpService;

        [ObservableProperty]
        private ReadOnlyCollection<DisplayCategory> _categories = default!;

        [ObservableProperty]
        private DisplayCategory _selectedCategory = default!;

        [ObservableProperty]
        private string _createCategoryName = string.Empty;

        [ObservableProperty]
        private bool _canCreateCategory = true;

        [ObservableProperty]
        private bool _canDeleteCategory = false;

        [RelayCommand]
        private async Task PageAppearingAsync() => await LoadDataAsync();

        [RelayCommand]
        private void SelectedCategoryChanged()
        {
            CanDeleteCategory = true;
        }

        [RelayCommand]
        private async Task CreateCategoryAsync()
        {
            if (!CanCreateCategory || string.IsNullOrWhiteSpace(CreateCategoryName))
            {
                return;
            }

            CreateCategory createCategory = new() { CategoryName = CreateCategoryName };

            if (await _httpService.CreateCategoryAsync(createCategory) is DisplayCategory)
            {
                await LoadDataAsync();
                CreateCategoryName = string.Empty;
            }
            else
            {
                await Shell.Current.DisplayAlert("Error!", $"Error creating {CreateCategoryName}.", "OK");
            }
        }

        [RelayCommand]
        private async Task DeleteCategoryAsync()
        {
            if (!CanDeleteCategory || SelectedCategory is null || SelectedCategory.CategoryId < 1)
            {
                return;
            }

            bool deleteConfirmed = await Shell.Current.DisplayAlert("Confirm delete", $"Are you sure you want to delete {SelectedCategory.CategoryName}?", "Yes", "No");

            if (deleteConfirmed)
            {
                await _httpService.DeleteCategoryAsync(SelectedCategory.CategoryId);
                await LoadDataAsync();
            }
        }

        private async Task LoadDataAsync() => Categories = await _httpService.GetCategoriesAsync();
    }
}
