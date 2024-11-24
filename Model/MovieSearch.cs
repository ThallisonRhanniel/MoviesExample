using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace MoviesExample.Model
{
    public class MovieSearch
    {
        [JsonProperty("totalResults")]
        public string TotalResults { get; set; }

        [JsonProperty("Search")]
        public List<MoviesSumary> Search { get; set; }

        public string Response { get; set; }
    }

    public class MoviesSumary
    {
        public string Title { get; set; }
        public string Year { get; set; }

        [JsonProperty("imdbID")]
        public string ImdbID { get; set; }
        public string Type { get; set; }
        public string Poster { get; set; }
    }
}
