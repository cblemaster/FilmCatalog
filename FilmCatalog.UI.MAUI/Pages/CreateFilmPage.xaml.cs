using FilmCatalog.UI.MAUI.PageModels;

namespace FilmCatalog.UI.MAUI.Pages;

public partial class CreateFilmPage : ContentPage
{
	public CreateFilmPage()
	{
		InitializeComponent();

        CreateFilmPageModel pageModel = Shell.Current.Handler!.MauiContext!.Services.GetService<CreateFilmPageModel>()!;
        BindingContext = pageModel;
    }
}