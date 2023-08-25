using System.Security.Cryptography;
using System.Text;

namespace MD5_Ripper
{
    // TODO: detect if hash is MD5 feature
    internal class Program
    {
        public const string LIST_10K_PATH = "C:\\Users\\chris\\Downloads\\10k-most-common.txt";

        public static string CreateMD5Hash(string input)
        {
            // Step 1, calculate MD5 hash from input
            MD5 md5 = System.Security.Cryptography.MD5.Create();
            byte[] inputBytes = System.Text.Encoding.UTF8.GetBytes(input);
            byte[] hashBytes = md5.ComputeHash(inputBytes);

            // Step 2, convert byte array to hex string
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hashBytes.Length; i++)
            {
                sb.Append(hashBytes[i].ToString("X2"));
            }
            return sb.ToString();
        }

        public static async Task ReadAllLinesIntoMemoryAsync()
        {
            var listArray = await File.ReadAllLinesAsync(LIST_10K_PATH);
            var list = listArray.ToList();
            foreach (var item in list)
            {
                Console.WriteLine(item);
            }
        }

        public static void PrintMD5Hashes()
        {
            string hash = CreateMD5Hash("password");
            Console.WriteLine(hash);
        }

        static async Task Main(string[] args)
        {
            //PrintMD5Hashes();

            await ReadAllLinesIntoMemoryAsync();
        }
    }
}