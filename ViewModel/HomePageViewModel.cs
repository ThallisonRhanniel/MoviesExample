using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using MoviesExample.Model;
using MoviesExample.Services;
using MoviesExample.View;

namespace MoviesExample.ViewModel
{
    public partial class HomePageViewModel : BaseViewModel
    {
        #region Property
        public ObservableCollection<MoviesSumary> Movies { get; } = [];

        [ObservableProperty] private Movie movie;

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(GetMoviesCommand))]
        private string _movieSearchParameter;

        private MoviesService _moviesService;

        private readonly IConnectivity _connectivity;

        [ObservableProperty] private bool _isLoading;
        #endregion



        public HomePageViewModel(MoviesService moviesService, IConnectivity connectivity)
        {
            _moviesService = moviesService;
            _connectivity = connectivity;
        }

        #region Methods


        [RelayCommand(CanExecute = nameof(CanGetMovies))]
        private async Task GetMoviesAsync()
        {
            try
            {
                if (_connectivity.NetworkAccess != NetworkAccess.Internet)
                {
                    await Shell.Current.DisplayAlert("No connectivity!",
                        "Please check internet and try again.", "OK");
                    return;
                }

                IsLoading = true;

                var moviesSearch = await _moviesService.GetMovies(MovieSearchParameter, pageNumber: 1);

                if (moviesSearch?.Search is null)
                {
                    await Shell.Current.DisplayAlert("Error!", "movie not found", "OK");
                    return;
                }

                foreach (var moviesSumary in moviesSearch.Search)
                {
                    Movies.Add(moviesSumary);
                }
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error!", ex.Message, "OK");
            }
            finally
            {
                IsLoading = false;
            }

        }


        [RelayCommand]
        private async Task NavigateMoviesDetailAsync(MoviesSumary movie)
        {
            try
            {
                IsLoading = true;
                var movieById = await _moviesService.GetMovieByImdbID(movie.ImdbID);


                if (movieById == null)
                    return;

                await Shell.Current.GoToAsync(nameof(MovieDetails), true, new Dictionary<string, object>
                {
                    {"Movie", movieById }
                });
            }
            finally
            {
                IsLoading = false;
            }
        }

        private bool CanGetMovies() => !string.IsNullOrWhiteSpace(MovieSearchParameter) && MovieSearchParameter.Length >= 3;

        [RelayCommand]
        private void ClearList() => Movies?.Clear();
        #endregion


    }
}
