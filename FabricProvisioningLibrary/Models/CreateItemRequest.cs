using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Microsoft.Fabric.Provisioning.Library.Models
{
    public class CreateItemRequest
    {
        [JsonPropertyName("displayName")]
        public required string DisplayName { get; set; }

        [JsonPropertyName("type")]
        public required string Type { get; set; }

        [JsonPropertyName("description")]
        public string? Description { get; set; }

        [JsonPropertyName("workspaceId")]
        public required string WorkspaceId { get; set; }

        [JsonPropertyName("definition")]
        public  ItemDefinition? Definition { get; set; }
    }
    public class ItemDefinition
    {
        [JsonPropertyName("format")]
        public string Format { get; set; }
        [JsonPropertyName("parts")]
        public ItemDefinitionPart[] Parts { get; set; }

    }
    public class ItemDefinitionPart
    {
        [JsonPropertyName("path")]
        public string Path { get; set; }
        [JsonPropertyName("payload")]
        public string Payload { get; set; }
        [JsonPropertyName("payloadType")]
        public string PayloadType { get; set; }


    }
}
