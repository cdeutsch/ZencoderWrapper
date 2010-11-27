using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZencoderWrapper
{
    public class AccountResponse
    {
        public string account_state { get; set; }
        public string plan { get; set; }
        public int minutes_used { get; set; }
        public int minutes_included { get; set; }
        public string billing_state { get; set; }
        public bool integration_mode { get; set; }
    }
}
