﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Microsoft.Fabric.Provisioning.Library.Models
{
    public class ListItemResponse
    {
        [JsonPropertyName("ContinuationToken ")]
        public  string ContinuationToken { get; set; }

        [JsonPropertyName("ContinuationUri ")]
        public  string ContinuationUri { get; set; }

        [JsonPropertyName("Value")]
        public required GetItemResponse[] Value { get; set; }
    }
}
