using FilmCatalog.UI.MAUI.PageModels;

namespace FilmCatalog.UI.MAUI.Pages;

public partial class GroupedFilmsPage : ContentPage
{
	public GroupedFilmsPage(GroupedFilmsPageModel pageModel)
	{
		InitializeComponent();

        BindingContext = pageModel;
	}
}