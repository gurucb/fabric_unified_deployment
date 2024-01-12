using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Microsoft.Fabric.Provisioning.Library.Models
{
    public class ListItemRequest
    {
        [Required]
        [JsonPropertyName("workspaceId ")]
        public required string WorkspaceId { get; set; }

        [JsonPropertyName("continuationToken")]
        public required string ContinuationToken { get; set; }

        [JsonPropertyName("type")]
        public required string Type { get; set; }
    }
}
