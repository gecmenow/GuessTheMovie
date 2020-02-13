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
        public Result[] SimResults { get; set; }

        [JsonProperty("total_pages")]
        public long TotalPages { get; set; }

        [JsonProperty("total_results")]
        public long TotalResults { get; set; }
    }

    public partial class SimResults
    {
        [JsonProperty("adult")]
        public bool Adult { get; set; }

        [JsonProperty("backdrop_path")]
        public string BackdropPath { get; set; }

        [JsonProperty("genre_ids")]
        public long[] GenreIds { get; set; }

        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("original_language")]
        public SimOriginalLanguage SimOriginalLanguage { get; set; }

        [JsonProperty("original_title")]
        public string OriginalTitle { get; set; }

        [JsonProperty("overview")]
        public string Overview { get; set; }

        [JsonProperty("poster_path")]
        public string PosterPath { get; set; }

        [JsonProperty("release_date")]
        public string ReleaseDate { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("video")]
        public bool Video { get; set; }

        [JsonProperty("vote_average")]
        public double VoteAverage { get; set; }

        [JsonProperty("vote_count")]
        public long VoteCount { get; set; }

        [JsonProperty("popularity")]
        public double Popularity { get; set; }
    }

    public enum SimOriginalLanguage { Cn, En, Ko, Hi };

    public partial class FilmSimilarVm
    {
        public static FilmSimilarVm FromJson(string json) => JsonConvert.DeserializeObject<FilmSimilarVm>(json, SimConverter.Settings);
    }

    public static class SimSerialize
    {
        public static string ToJson(this FilmSimilarVm self) => JsonConvert.SerializeObject(self, SimConverter.Settings);
    }

    internal static class SimConverter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters =
            {
                SimOriginalLanguageConverter.Singleton,
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }

    internal class SimOriginalLanguageConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(SimOriginalLanguage) || t == typeof(SimOriginalLanguage?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            switch (value)
            {
                case "cn":
                    return SimOriginalLanguage.Cn;
                case "en":
                    return SimOriginalLanguage.En;
                case "ko":
                    return SimOriginalLanguage.Ko;
                case "hi":
                    return SimOriginalLanguage.Hi;
                default:
                    return SimOriginalLanguage.En;
            }
            throw new Exception("Cannot unmarshal type SimOriginalLanguage");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (SimOriginalLanguage)untypedValue;
            switch (value)
            {
                case SimOriginalLanguage.Cn:
                    serializer.Serialize(writer, "cn");
                    return;
                case SimOriginalLanguage.En:
                    serializer.Serialize(writer, "en");
                    return;
                case SimOriginalLanguage.Ko:
                    serializer.Serialize(writer, "ko");
                    return;
                case SimOriginalLanguage.Hi:
                    serializer.Serialize(writer, "hi");
                    return;
            }
            throw new Exception("Cannot marshal type SimOriginalLanguage");
        }

        public static readonly SimOriginalLanguageConverter Singleton = new SimOriginalLanguageConverter();
    }

    public partial class FilmSimilarVM
    {
        public static FilmSimilarVM FromJson(string json) => JsonConvert.DeserializeObject<FilmSimilarVM>(json, SimConverter.Settings);
    }
}