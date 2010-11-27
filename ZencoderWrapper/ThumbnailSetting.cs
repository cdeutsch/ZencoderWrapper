using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace ZencoderWrapper
{
    public class ThumbnailSetting
    {
        public int? number { get; set; }
        public bool? start_at_first_frame { get; set; }
        public int? interval { get; set; }
        public string size { get; set; }
        public string base_url { get; set; }
        public string prefix { get; set; }
        public string format { get; set; }

        [JsonProperty(PropertyName = "public")]
        public bool? public_s3 { get; set; }

        public List<AccessControlSetting> access_control { get; set; }
    }
}
