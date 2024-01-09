using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Microsoft.Fabric.Provisioning.Library.Models
{
    public class GetWorkspaceRequest
    {

        [JsonPropertyName("WorkspaceId")]
        public required string WorkspaceId { get; set; }
    }
}
