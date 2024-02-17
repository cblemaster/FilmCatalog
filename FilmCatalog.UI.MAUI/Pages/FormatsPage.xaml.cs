using FilmCatalog.UI.MAUI.PageModels;

namespace FilmCatalog.UI.MAUI.Pages;

public partial class FormatsPage : ContentPage
{
    public FormatsPage(FormatsPageModel pageModel)
    {
        InitializeComponent();
        BindingContext = pageModel;
    }
}