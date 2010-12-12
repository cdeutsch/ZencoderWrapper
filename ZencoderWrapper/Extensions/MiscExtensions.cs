using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace ZencoderWrapper.Extensions
{
    public static class MiscExtensions
    {
        public static void ForceCanonicalPathAndQuery(Uri uri)
        {
            string paq = uri.PathAndQuery; // need to access PathAndQuery
            FieldInfo flagsFieldInfo = typeof(Uri).GetField("m_Flags", BindingFlags.Instance | BindingFlags.NonPublic);
            ulong flags = (ulong)flagsFieldInfo.GetValue(uri);
            flags &= ~((ulong)0x30); // Flags.PathNotCanonical|Flags.QueryNotCanonical
            flagsFieldInfo.SetValue(uri, flags);
        }
    }
}
