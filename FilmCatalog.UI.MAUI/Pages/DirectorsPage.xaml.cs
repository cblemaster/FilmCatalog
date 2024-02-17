using FilmCatalog.UI.MAUI.PageModels;

namespace FilmCatalog.UI.MAUI.Pages;

public partial class DirectorsPage : ContentPage
{
    public DirectorsPage(DirectorsPageModel pageModel)
    {
        InitializeComponent();
        BindingContext = pageModel;
    }
}