using FilmCatalog.UI.MAUI.PageModels;

namespace FilmCatalog.UI.MAUI.Pages;

public partial class CategoriesPage : ContentPage
{
	public CategoriesPage(CategoriesPageModel pageModel)
	{
		InitializeComponent();
        BindingContext = pageModel;
    }
}