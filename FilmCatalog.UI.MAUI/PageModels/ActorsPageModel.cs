using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FilmCatalog.UI.MAUI.Models;
using FilmCatalog.UI.MAUI.Services;
using System.Collections.ObjectModel;

namespace FilmCatalog.UI.MAUI.PageModels
{
    public partial class ActorsPageModel : ObservableObject
    {
        private readonly IHttpService _httpService;

        public ActorsPageModel(IHttpService httpService)
        {
            _httpService = httpService;
        }

        [ObservableProperty]
        private ReadOnlyCollection<DisplayActor> _actors = default!;

        [RelayCommand]
        private void PageAppearing() => LoadData();

        private async void LoadData()
        {
            Actors = await _httpService.GetActors();
        }
    }
}
