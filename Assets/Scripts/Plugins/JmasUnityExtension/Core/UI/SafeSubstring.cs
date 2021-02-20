using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;


namespace Jmas
{
    public static class SafeSubstring
    {
        public static string Sub(this string a, int begin)
        {
            return a.Length <= begin ? string.Empty : a.Substring(begin);
        }
        public static string Sub(this string a, int begin, int length)
        {
            return a.Length <= begin ? string.Empty : a.Substring(begin, length);
        }
    }
}
