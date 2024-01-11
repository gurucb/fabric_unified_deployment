using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Microsoft.Fabric.Provisioning.Library.Models
{
    public class GetItemResponse
    {
        [JsonPropertyName("id")]
        public required string Id { get; set; }
        [JsonPropertyName("type")]
        public required string Type { get; set; }
        [JsonPropertyName("displayName")]
        public required string DisplayName { get; set; }
        [JsonPropertyName("description")]
        public required string Description { get; set; }
        [JsonPropertyName("workspaceId")]
        public required string WorkspaceId { get; set; }

    }
}
