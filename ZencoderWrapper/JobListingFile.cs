using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZencoderWrapper
{
    public class JobListingFile
    {
        public DateTime? created_at { get; set; }
        public DateTime? updated_at { get; set; }
        public string url { get; set; }
        public int? id { get; set; }
    }
}
