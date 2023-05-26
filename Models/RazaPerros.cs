using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json.Serialization;
namespace appPetech.Models
{
    public class RazaPerros
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("breed")]
        public string Breed { get; set; }

        [JsonPropertyName("origin")]
        public string Origin { get; set; }

        [JsonPropertyName("url")]
        public string Url { get; set; }

        [JsonPropertyName("img")]
        public string Img { get; set; }

        /*[JsonPropertyName("meta")]
        public Dictionary<string, object> Meta { get; set; }*/
    }
}