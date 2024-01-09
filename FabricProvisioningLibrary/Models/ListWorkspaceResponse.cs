using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Microsoft.Fabric.Provisioning.Library.Models
{
    public class ListWorkspaceResponse
    {
        [JsonPropertyName("ContinuationToken ")]
        public required string ContinuationToken { get; set; }

        [JsonPropertyName("ContinuationUri ")]
        public required string ContinuationUri  { get; set; }

        [JsonPropertyName("Value")]
        public required WorkspaceResponse[] Value { get; set; }
    }
}
