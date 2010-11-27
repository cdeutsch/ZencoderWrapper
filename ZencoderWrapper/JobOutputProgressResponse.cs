using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZencoderWrapper
{
    public class JobOutputProgressResponse
    {
        public string state { get; set; }
        public string current_event { get; set; }
        public string progress { get; set; }
    }
}
