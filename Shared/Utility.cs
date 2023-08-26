using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Shared
{
    public static class Utility
    {
        private static readonly Regex MD5Regex = new("^[a-fA-F0-9]{32}$");

        public static string CreateMD5Hash(string input)
        {
            using MD5 md5 = MD5.Create();

            byte[] inputBytes = Encoding.UTF8.GetBytes(input);
            byte[] hashBytes = md5.ComputeHash(inputBytes);

            return Convert.ToHexString(hashBytes);
        }

        public static bool IsMD5Hash(string hash)
        {
            return MD5Regex.IsMatch(hash);
        }
    }
}