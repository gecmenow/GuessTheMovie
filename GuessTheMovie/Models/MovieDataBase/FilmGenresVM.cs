using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace GuessTheMovie.Models.MovieDataBase
{
    public partial class FilmGenresVM
    {
        [JsonProperty("adult")]
        public bool Adult { get; set; }

        [JsonProperty("backdrop_path")]
        public string BackdropPath { get; set; }

        [JsonProperty("belongs_to_collection")]
        public object BelongsToCollection { get; set; }

        [JsonProperty("budget")]
        public long Budget { get; set; }

        [JsonProperty("genres")]
        public Genre[] Genres { get; set; }

        [JsonProperty("homepage")]
        public Uri Homepage { get; set; }

        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("imdb_id")]
        public string ImdbId { get; set; }

        [JsonProperty("original_language")]
        public string OriginalLanguage { get; set; }

        [JsonProperty("original_title")]
        public string OriginalTitle { get; set; }

        [JsonProperty("overview")]
        public string Overview { get; set; }

        [JsonProperty("popularity")]
        public double Popularity { get; set; }

        [JsonProperty("poster_path")]
        public string PosterPath { get; set; }

        [JsonProperty("production_companies")]
        public ProductionCompany[] ProductionCompanies { get; set; }

        [JsonProperty("production_countries")]
        public ProductionCountry[] ProductionCountries { get; set; }

        [JsonProperty("release_date")]
        public DateTimeOffset ReleaseDate { get; set; }

        [JsonProperty("revenue")]
        public long Revenue { get; set; }

        [JsonProperty("runtime")]
        public long Runtime { get; set; }

        [JsonProperty("spoken_languages")]
        public SpokenLanguage[] SpokenLanguages { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("tagline")]
        public string Tagline { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("video")]
        public bool Video { get; set; }

        [JsonProperty("vote_average")]
        public long VoteAverage { get; set; }

        [JsonProperty("vote_count")]
        public long VoteCount { get; set; }
    }

    public partial class Genre
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }

    public partial class ProductionCompany
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("logo_path")]
        public string LogoPath { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("origin_country")]
        public string OriginCountry { get; set; }
    }

    public partial class ProductionCountry
    {
        [JsonProperty("iso_3166_1")]
        public string Iso3166_1 { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }

    public partial class SpokenLanguage
    {
        [JsonProperty("iso_639_1")]
        public string Iso639_1 { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }

    public partial class FilmGenresVM
    {
        public static FilmGenresVM FromJson(string json) => JsonConvert.DeserializeObject<FilmGenresVM>(json, ConverterFilmGenres.Settings);
    }

    public static class SerializeFilmGenres
    {
        public static string ToJson(this FilmGenresVM self) => JsonConvert.SerializeObject(self, ConverterFilmGenres.Settings);
    }

    internal static class ConverterFilmGenres
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters =
            {
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }
}