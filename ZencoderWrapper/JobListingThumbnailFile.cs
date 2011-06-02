using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZencoderWrapper
{
    public class JobListingThumbnailFile : JobListingFile
    {
        public string group_label { get; set; }
        public string format { get; set; }
        public int? height { get; set; }
        public int? width { get; set; }
        public long? file_size_bytes { get; set; }
    }
}
