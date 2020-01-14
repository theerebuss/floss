using System.Text.Json.Serialization;

namespace floss.Npms
{
    public class Links
    {
        [JsonPropertyName("repository")]
        public string Repository { get; set; }
    }

    public class Metadata
    {
        [JsonPropertyName("links")]
        public Links Links { get; set; }
    }

    public class Collected
    {
        [JsonPropertyName("metadata")]
        public Metadata Metadata { get; set; }
    }

    public class NpmsObject
    {
        [JsonPropertyName("collected")]
        public Collected Collected { get; set; }
    }
}