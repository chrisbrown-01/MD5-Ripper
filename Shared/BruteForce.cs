using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Shared.Utility;

namespace Shared
{
    // TODO: update README with brute force benchmark results
    public static class BruteForce
    {
        private const int MAX_PASSWORD_LENGTH = 5;
        private static readonly char[] PasswordCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789!\"#$%&'()*+,-./:;<=>?@[\\]^_`{|}~".ToCharArray();
        private static string Password = string.Empty;
        private static bool PasswordCracked = false;

        /// <summary>
        /// Bruteforce every possible character combination (up to MAX_PASSWORD_LENGTH) that does not include a space character.
        /// </summary>
        /// <param name="hash"></param>
        public static void BruteForcePasswordGivenHash(string hash)
        {
            for (int length = 1; length <= MAX_PASSWORD_LENGTH; length++)
            {
                Parallel.For(0, PasswordCharacters.Length, i =>
                {
                    var charArray = new char[length];
                    charArray[0] = PasswordCharacters[i];
                    CheckForMatchingHash(charArray, length, 1, hash);
                });
            }

            if (PasswordCracked && !String.IsNullOrEmpty(Password))
            {
                Console.WriteLine($"Hash cracked: password is {Password}");
            }
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

        private static void CheckForMatchingHash(char[] charArray, int maxLength, int index, string hash)
        {
            if (PasswordCracked) return;

            if (index == maxLength)
            {
                //Console.WriteLine(new string(charArray));

                if (string.Equals(CreateMD5Hash(new string(charArray)), hash, StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine($"MATCH FOUND: PLAINTEXT PASSWORD IS {new string(charArray)}");
                    Password = new string(charArray);
                    PasswordCracked = true;
                }

                return;
            }
            for (int i = 0; i < PasswordCharacters.Length; i++)
            {
                charArray[index] = PasswordCharacters[i];
                CheckForMatchingHash(charArray, maxLength, index + 1, hash);
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