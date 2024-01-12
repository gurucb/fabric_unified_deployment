using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Microsoft.Fabric.Provisioning.Library.Models
{
    public class UpdateItemRequest
    {
        [Required]
        [JsonPropertyName("workspaceId")]
        public required string WorkspaceId { get; set; }
        [Required]
        [JsonPropertyName("itemId")]
        public required string ItemId { get; set; }
        [JsonPropertyName("description")]
        public  string? Description { get; set; }
        [JsonPropertyName("displayName")]
        public  string? DisplayName { get; set; }
    }
}
