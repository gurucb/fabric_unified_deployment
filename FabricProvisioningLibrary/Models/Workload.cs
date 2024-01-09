namespace Microsoft.Fabric.Provisioning.Library.Models
{
    using System.Text.Json.Serialization;

    public class Workload
    {
        [JsonPropertyName("id")]
        public required string Id { get; set; }

        [JsonPropertyName("type")]
        public required string Type { get; set; }

        [JsonPropertyName("displayName")]
        public required string DisplayName { get; set; }

        [JsonPropertyName("description")]
        public required string Description { get; set; }
    }
}
