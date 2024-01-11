using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Microsoft.Fabric.Provisioning.Library.Models
{
    public class GetItemRequest
    {
        [JsonPropertyName("workspaceId")]
        public required string WorkspaceId { get; set; }
        [JsonPropertyName("itemId")]
        public required string itemId { get; set; }

    }
}
