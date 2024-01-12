using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Microsoft.Fabric.Provisioning.Library.Models
{
    public class UpdateItemResponse
    {
        [JsonPropertyName("description")]
        public required string Description { get; set; }
        [JsonPropertyName("displayName")]
        public required string DisplayName { get; set; }
        [JsonPropertyName("id")]
        public required string ID { get; set; }
        [JsonPropertyName("type")]
        public required string Type { get; set; }
        [JsonPropertyName("workspaceId")]
        public required string WorkspaceId { get; set; }
    }
}
