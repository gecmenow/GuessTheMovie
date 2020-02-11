using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace GuessTheMovie.Models.MovieDataBase
{
    public partial class FilmImagesVM
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("backdrops")]
        public Backdrop[] Backdrops { get; set; }

        [JsonProperty("posters")]
        public Backdrop[] Posters { get; set; }
    }

    public partial class Backdrop
    {
        [JsonProperty("aspect_ratio")]
        public double AspectRatio { get; set; }

        [JsonProperty("file_path")]
        public string FilePath { get; set; }

        [JsonProperty("height")]
        public long Height { get; set; }

        [JsonProperty("iso_639_1")]
        public string Iso639_1 { get; set; }

        [JsonProperty("vote_average")]
        public double VoteAverage { get; set; }

        [JsonProperty("vote_count")]
        public long VoteCount { get; set; }

        [JsonProperty("width")]
        public long Width { get; set; }
    }

    public partial class FilmImagesVM
    {
        public static FilmImagesVM FromJson(string json) => JsonConvert.DeserializeObject<FilmImagesVM>(json, ConverterImages.Settings);
    }

    public static class SerializeImages
    {
        public static string ToJson(this FilmImagesVM self) => JsonConvert.SerializeObject(self, ConverterImages.Settings);
    }

    internal static class ConverterImages
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