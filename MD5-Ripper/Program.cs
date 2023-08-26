using Shared;
using System.Security.Cryptography;
using System.Text;
using static Shared.FilePaths;
using static Shared.MD5_Helpers;

namespace MD5_Ripper
{
    // TODO: detect if hash is MD5 feature
    // TODO: precomputed hashtable/dictionary of rockyou, stored in file or in app?
    internal class Program
    {
        internal static void Main(string[] args)
        {
            PrintMD5HashAsync();
        }

        internal static void PrintMD5HashAsync()
        {
            string hash = CreateMD5Hash("password");
            Console.WriteLine(hash);
        }
    }
}