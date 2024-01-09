namespace Microsoft.Fabric.Provisioning.Library.Models
{
    using System.Text.Json.Serialization;

    public class WorkspaceItem
    {
        [JsonPropertyName("workspace_name")]
        public required string WorkspaceName { get; set; }

        [JsonPropertyName("workspace_id")]
        public required string WorkspaceId { get; set; }

        [JsonPropertyName("item")]
        public required Workload Item { get; set; }
    }
}
