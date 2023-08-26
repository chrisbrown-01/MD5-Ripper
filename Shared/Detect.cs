using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Shared
{
    public static class Detect
    {
        private static readonly Regex MD5Regex = new("^[a-fA-F0-9]{32}$");

        public static bool IsMD5Hash(string hash)
        {
            return MD5Regex.IsMatch(hash);
        }
    }
}