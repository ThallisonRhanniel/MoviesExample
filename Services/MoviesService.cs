using System.Diagnostics;
using System.Net.Http.Json;
using System.Text.RegularExpressions;
using MoviesExample.Helpers;
using MoviesExample.Model;

namespace MoviesExample.Services
{
    public class MoviesService
    {
        private HttpClient _httpClient;
        public MoviesService()
        {
            _httpClient = new HttpClient();
        }

        private MovieSearch? _moviesList;

        private string Sanitizer(string movieTitle) => Regex.Replace(movieTitle, "[^0-9a-zA-Z]+", "");


        public async Task<MovieSearch?> GetMovies(string MovieName, int pageNumber)
        {
            try
            {
                var response = await _httpClient.GetAsync($"{Constants.EndPoints.BaseUrl}/?s={Sanitizer(MovieName)}&apikey={Constants.Api.Key}&page={pageNumber}");

                if (response.IsSuccessStatusCode)
                    _moviesList = await response.Content.ReadFromJsonAsync<MovieSearch>();

            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Unable to launch maps: {ex.Message}");
            }

            return _moviesList ?? null;
        }

        public async Task<Movie?> GetMovieByImdbID(string ImdbId = "tt11126994")
        {
            try
            {
                var response = await _httpClient.GetAsync($"{Constants.EndPoints.BaseUrl}/?i={ImdbId}&apikey={Constants.Api.Key}");

                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<Movie>();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Unable to launch maps: {ex.Message}");
            }

            return null;
        }
    }
}
