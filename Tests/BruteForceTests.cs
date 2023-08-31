using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests
{
    public class BruteForceTests
    {
        [Fact]
        public void BruteforcedHash_Should_Equal_UtilityHash()
        {
            var charArray = new char[2];
            charArray[0] = 'a';
            charArray[1] = 'a';

            var hash = CreateMD5Hash("aa");

            var stringComparison = string.Equals(Utility.CreateMD5Hash(new string(charArray)), hash, StringComparison.OrdinalIgnoreCase);

            stringComparison.Should().BeTrue();
        }
    }
}