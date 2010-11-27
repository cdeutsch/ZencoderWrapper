using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZencoderWrapper
{
    public class JobResponse
    {
        public int id { get; set; }
        public bool test { get; set; }
        public List<OutputResponse> outputs { get; set; }
    }
}
