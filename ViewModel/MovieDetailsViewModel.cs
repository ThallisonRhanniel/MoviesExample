using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MoviesExample.Model;

namespace MoviesExample.ViewModel
{
    [QueryProperty(nameof(Movie), "Movie")]
    public partial class MovieDetailsViewModel : BaseViewModel
    {
        [ObservableProperty] private Movie _movie;
        
        partial void OnMovieChanged(Movie value) => Title = value.Title;

        [RelayCommand]
        private async Task ShareMovie()
        {
            if (string.IsNullOrEmpty(Movie.Website) || Movie.Website == "N/A")
            {
                await ShareText(Movie.Title, Movie.Title);
                return;
            }

            await ShareUri(Movie.Title, Movie.Website);
        }

        private async Task ShareText(string title , string text)
        {
            await Share.Default.RequestAsync(new ShareTextRequest
            {
                Text = text,
                Title = title
            });
        }

        private async Task ShareUri(string title,string uri)
        {
            await Share.Default.RequestAsync(new ShareTextRequest
            {
                Text = uri,
                Title = title
            });
        }
    }
}
