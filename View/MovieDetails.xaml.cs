using MoviesExample.ViewModel;

namespace MoviesExample.View;

public partial class MovieDetails : ContentPage
{
	public MovieDetails(MovieDetailsViewModel viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;
    }
}