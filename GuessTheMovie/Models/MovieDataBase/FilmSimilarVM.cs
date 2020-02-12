using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace GuessTheMovie.Models.MovieDataBase
{
    public partial class FilmSimilarVM
    {
        [JsonProperty("page")]
        public long Page { get; set; }

        [JsonProperty("results")]
        public Result[] Results { get; set; }

        [JsonProperty("total_pages")]
        public long TotalPages { get; set; }

        [JsonProperty("total_results")]
        public long TotalResults { get; set; }
    }

    public partial class FilmSimilarVM
    {
        public static FilmSimilarVM FromJson(string json) => JsonConvert.DeserializeObject<FilmSimilarVM>(json, Converter.Settings);
    }
}