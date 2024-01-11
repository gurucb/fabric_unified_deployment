﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Microsoft.Fabric.Provisioning.Library.Models
{
    public class GetWorkspaceResponse : WorkspaceResponse
    {
        [JsonPropertyName("capacityAssignmentProgress")]
        public required string capacityAssignmentProgress { get; set; }
    }
}
