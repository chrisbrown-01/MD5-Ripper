using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests
{
    public class HelpersTests
    {
        [Theory]
        [InlineData("password", "5F4DCC3B5AA765D61D8327DEB882CF99")]
        [InlineData("rockyou_123!", "853858F1E3E63765F51CB5BBBB0F4743")]
        [InlineData("PR445#EdFY*1k$s!%j8C", "6F82818727870F99D51AF9669BC837C1")]
        public void Test_CreateMD5Hash(string plaintext, string hash)
        {
            CreateMD5Hash(plaintext).Should().Be(hash);
        }
    }
}