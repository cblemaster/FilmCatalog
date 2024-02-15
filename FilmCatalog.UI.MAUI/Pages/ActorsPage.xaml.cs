using FilmCatalog.UI.MAUI.PageModels;

namespace FilmCatalog.UI.MAUI.Pages;

public partial class ActorsPage : ContentPage
{
	public ActorsPage(ActorsPageModel pageModel)
	{
		InitializeComponent();
        BindingContext = pageModel;
	}
}