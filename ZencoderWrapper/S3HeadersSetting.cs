using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace ZencoderWrapper
{
    public class S3HeadersSetting
    {
        [JsonProperty(PropertyName = "Cache-Control")]
        public string cache_control {get;set;}

        [JsonProperty(PropertyName = "Content-Disposition")]
        public string content_disposition { get; set; }

        [JsonProperty(PropertyName = "Content-Encoding")]
        public string content_encoding { get; set; }

        [JsonProperty(PropertyName = "Content-Type")]
        public string content_type { get; set; }

        [JsonProperty(PropertyName = "Expires")]
        public string expires { get; set; }

        [JsonProperty(PropertyName = "x-amz-acl")]
        public string x_amz_acl { get; set; }

        [JsonProperty(PropertyName = "x-amz-storage-class")]
        public string x_amz_storage_class { get; set; }

    }
}
