using FilmCatalog.UI.MAUI.PageModels;

namespace FilmCatalog.UI.MAUI.Pages;

public partial class FilmsPage : ContentPage
{
    public FilmsPage(FilmsPageModel pageModel)
    {
        InitializeComponent();
        BindingContext = pageModel;
    }
}