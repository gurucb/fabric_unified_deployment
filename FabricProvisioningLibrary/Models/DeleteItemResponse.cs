﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Microsoft.Fabric.Provisioning.Library.Models
{
    public class DeleteItemResponse
    {
        [JsonPropertyName("statusCode")]
        public required string StatusCode { get; set; }
    }
}
