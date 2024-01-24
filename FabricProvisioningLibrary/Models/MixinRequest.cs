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
        [JsonPropertyName("displayName")]
        public required string DisplayName { get; set; }

        [JsonPropertyName("description")]
        public required string Description { get; set; }
        [JsonPropertyName("capacityId")]
        public required string CapacityId { get; set; }
    }

}
