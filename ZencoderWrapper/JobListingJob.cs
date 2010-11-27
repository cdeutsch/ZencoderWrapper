using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZencoderWrapper
{
    public class JobListingJob
    {
        public DateTime? created_at { get; set; }
        public DateTime? finished_at { get; set; }
        public DateTime? updated_at { get; set; }
        public DateTime? submitted_at { get; set; }
        public string pass_through { get; set; }
        public int id { get; set; }
        public bool test { get; set; }
        public string state { get; set; }
        public JobListingMediaFile input_media_file { get; set; }
        public List<JobListingOutputMediaFile> output_media_files { get; set; }
        public List<JobListingFile> thumbnails { get; set; }
    }
}
