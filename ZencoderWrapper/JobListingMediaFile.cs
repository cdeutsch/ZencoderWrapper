using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZencoderWrapper
{
    public class JobListingMediaFile : JobListingFile
    {
        public string format { get; set; }        
        public int? frame_rate { get; set; }
        public DateTime? finished_at { get; set; }        
        public long? duration_in_ms { get; set; }
        public int? audio_sample_rate { get; set; }                
        public string error_message { get; set; }
        public string error_class { get; set; }
        public int? audio_bitrate_in_kbps { get; set; }
        public string audio_codec { get; set; }
        public int? height { get; set; }
        public long? file_size_bytes { get; set; }
        public string video_codec { get; set; }
        public bool test { get; set; }
        public string channels { get; set; }
        public int? width { get; set; }
        public int? video_bitrate_in_kbps { get; set; }
        public string state { get; set; }       
    }
}
