using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    // TODO: set limit of between 5 and 10 character length
    public static class BruteForce
    {
        private static readonly char[] PasswordCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789!\"#$%&'()*+,-./:;<=>?@[\\]^_`{|}~".ToCharArray();

        public static void BruteforcePasswordGivenHash(string hash)
        {
            var upperCaseHash = hash.ToUpper();
        }

        public static void GenerateAllPasswordsOfLength_MultiThreaded_WithStringBuilder(int maxLength)
        {
            if (maxLength < 1)
            {
                throw new ArgumentException("maxLength must be greater than 0");
            }
            for (int length = 1; length <= maxLength; length++)
            {
                Parallel.For(0, PasswordCharacters.Length, i =>
                {
                    var sb = new StringBuilder(maxLength);
                    sb.Append(PasswordCharacters[i]);
                    GeneratePassword_MultiThreaded_WithStringBuilder(sb, length, 1);
                });
            }
        }

        public static void GenerateAllPasswordsOfLength_SingleThreaded_WithStringBuilder(int maxLength)
        {
            if (maxLength < 1)
            {
                throw new ArgumentException("maxLength must be greater than 0");
            }
            var sb = new StringBuilder(maxLength);
            for (int length = 1; length <= maxLength; length++)
            {
                GeneratePassword_SingleThreaded_WithStringBuilder(sb, length, 0);
            }
        }

        public static void GenerateAllPasswordsOfLength_SingleThreaded(int maxLength)
        {
            if (maxLength < 1)
            {
                throw new ArgumentException("maxLength must be greater than 0");
            }
            for (int length = 1; length <= maxLength; length++)
            {
                GeneratePassword_SingleThreaded(new char[length], length, 0);
            }
        }

        // Fastest solution
        public static void GenerateAllPasswordsOfLength_MultiThreaded(int maxLength)
        {
            if (maxLength < 1)
            {
                throw new ArgumentException("maxLength must be greater than 0");
            }
            for (int length = 1; length <= maxLength; length++)
            {
                Parallel.For(0, PasswordCharacters.Length, i =>
                {
                    var charArray = new char[length];
                    charArray[0] = PasswordCharacters[i];
                    GeneratePassword_MultiThreaded(charArray, length, 1);
                });
            }
        }

        private static void GeneratePassword_MultiThreaded(char[] charArray, int maxLength, int index)
        {
            if (index == maxLength)
            {
                new string(charArray);
                //Console.WriteLine(new string(charArray));
                return;
            }
            for (int i = 0; i < PasswordCharacters.Length; i++)
            {
                charArray[index] = PasswordCharacters[i];
                GeneratePassword_MultiThreaded(charArray, maxLength, index + 1);
            }
        }

        private static void GeneratePassword_MultiThreaded_WithStringBuilder(StringBuilder sb, int maxLength, int index)
        {
            if (index == maxLength)
            {
                //Console.WriteLine(sb.ToString());
                sb.ToString();
                return;
            }
            for (int i = 0; i < PasswordCharacters.Length; i++)
            {
                sb.Append(PasswordCharacters[i]);
                GeneratePassword_MultiThreaded_WithStringBuilder(sb, maxLength, index + 1);
                sb.Length--;
            }
        }

        private static void GeneratePassword_SingleThreaded_WithStringBuilder(StringBuilder sb, int maxLength, int index)
        {
            if (index == maxLength)
            {
                //Console.WriteLine(sb.ToString());
                sb.ToString();
                return;
            }
            for (int i = 0; i < PasswordCharacters.Length; i++)
            {
                sb.Append(PasswordCharacters[i]);
                GeneratePassword_SingleThreaded_WithStringBuilder(sb, maxLength, index + 1);
                sb.Length--;
            }
        }

        private static void GeneratePassword_SingleThreaded(char[] charArray, int maxLength, int index)
        {
            if (index == maxLength)
            {
                //Console.WriteLine(new string(charArray));
                new string(charArray);
                return;
            }
            for (int i = 0; i < PasswordCharacters.Length; i++)
            {
                charArray[index] = PasswordCharacters[i];
                GeneratePassword_SingleThreaded(charArray, maxLength, index + 1);
            }
        }
    }
}