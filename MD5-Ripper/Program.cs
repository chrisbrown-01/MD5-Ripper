using Shared;
using System.Security.Cryptography;
using System.Text;
using static Shared.FilePaths;
using static Shared.Utility;

namespace MD5_Ripper
{
    // TODO: precomputed compressed hashtable/dictionary of rockyou, stored in file or in app?
    internal class Program
    {
        internal static async Task Main(string[] args)
        {
            //PrintMD5HashAsync();

            await GenerateHashedList.CreateHashesFileAsync_StreamReader(LIST_10K_PATH, LIST_10K_HASHED_PATH);
        }

        internal static void PrintMD5HashAsync()
        {
            string hash = CreateMD5Hash("password");
            Console.WriteLine(hash);
        }
    }
}