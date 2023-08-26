namespace Tests
{
    public class UtilityTests
    {
        [Theory]
        [InlineData("d41d8cd98f00b204e9800998ecf8427e", true)]
        [InlineData("0cc175b9c0f1b6a831c399e269772661", true)]
        [InlineData("900150983cd24fb0d6963f7d28e17f72", true)]
        [InlineData("f96b697d7cb7938d525a2f31aaf161d0", true)]
        [InlineData("5d41402abc4b2a76b9719d911017c592", true)]
        [InlineData("202cb962ac59075b964b07152d234b70", true)]
        [InlineData("e10adc3949ba59abbe56e057f20f883e", true)]
        [InlineData("827ccb0eea8a706c4c34a16891f84e7b", true)]
        [InlineData("827xyz0eea8a706c4c34a16891f84e7b", false)]
        [InlineData("e10adc3949ba59abbe56e057f20f883", false)]
        [InlineData("202cb962ac59075b964b07152d234b703", false)]
        [InlineData("test", false)]
        [InlineData("", false)]
        public void Theory_IsMD5Hash(string hash, bool expected)
        {
            IsMD5Hash(hash).Should().Be(expected);
        }

        [Theory]
        [InlineData("password", "5F4DCC3B5AA765D61D8327DEB882CF99")]
        [InlineData("rockyou_123!", "853858F1E3E63765F51CB5BBBB0F4743")]
        [InlineData("PR445#EdFY*1k$s!%j8C", "6F82818727870F99D51AF9669BC837C1")]
        public void Theory_CreateMD5Hash(string plaintext, string hash)
        {
            CreateMD5Hash(plaintext).Should().Be(hash);
        }
    }
}