using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Microsoft.Fabric.Provisioning.Library.Models
{
    public class MixinRequest
    {
        [JsonPropertyName("workspaceDisplayName")]
        public required string WorkspaceDisplayName { get; set; }

        [JsonPropertyName("workspaceDescription")]
        public required string WorkspaceDescription { get; set; }
        [JsonPropertyName("itemDisplayName")]
        public required string ItemDisplayName { get; set; }

        [JsonPropertyName("itemType")]
        public required string ItemType { get; set; }
        //[JsonPropertyName("workspaceId")]
        //public required string WorkspaceId { get; set; }
        [JsonPropertyName("capacityId")]
        public required string CapacityId { get; set; }
    }

}
