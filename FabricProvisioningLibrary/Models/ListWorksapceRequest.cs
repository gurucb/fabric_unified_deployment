using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Microsoft.Fabric.Provisioning.Library.Models
{
    public class ListWorksapceRequest
    {
        
        [JsonPropertyName("ContinuationToken")]
        public  string? ContinuationToken { get; set; }
    }
}
