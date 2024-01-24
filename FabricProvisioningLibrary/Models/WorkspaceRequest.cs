namespace Microsoft.Fabric.Provisioning.Library.Models
{
    using System.Text.Json.Serialization;

    public class WorkspaceRequest
    {
        [JsonPropertyName("displayName")]
        public required string DisplayName { get; set; }

        [JsonPropertyName("description")]
        public required string Description { get; set; }

        [JsonPropertyName("capacityId")]
        public required string CapacityId { get; set; }
    }
}
