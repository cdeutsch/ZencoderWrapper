using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZencoderWrapper
{
    public class AccessControlSetting
    {
        public string grantee { get; set; }
        public List<string> permissions { get; set; }
    }
}
