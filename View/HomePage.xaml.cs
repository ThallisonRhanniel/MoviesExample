using MoviesExample.ViewModel;

namespace MoviesExample.View;

public partial class HomePage : ContentPage
{
	public HomePage(HomePageViewModel viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;
    }

    private void MoviesCollectionView_OnSelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        var cv = sender as CollectionView;
        if (cv?.SelectedItem == null)
            return;

        cv.SelectedItem = null;
    }
}