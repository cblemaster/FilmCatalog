using FilmCatalog.UI.MAUI.Models;
using FilmCatalog.UI.MAUI.PageModels;

namespace FilmCatalog.UI.MAUI.Pages;

public partial class FilmDetailsPage : ContentPage
{
	public FilmDetailsPage(DisplayFilm film)
	{
		InitializeComponent();
        FilmDetailsPageModel pageModel = Shell.Current.Handler!.MauiContext!.Services.GetService<FilmDetailsPageModel>()!;
        BindingContext = pageModel;
        pageModel.Film = film;
	}
}