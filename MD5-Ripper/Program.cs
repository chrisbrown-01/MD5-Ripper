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
        public static async Task Main(string[] args)
        {
            //PrintMD5HashAsync();

            //await GenerateHashedList.CreateHashesFileAsync_StreamReader(LIST_10K_PATH, LIST_10K_HASHED_PATH);
            //await GenerateHashedList.CreateHashesFileAsync_StreamReader(LIST_ROCKYOU_PATH, LIST_ROCKYOU_HASHED_PATH);

            //await GenerateHashedList.CreateHashesFileAsync_StreamReader_BinFile(LIST_10K_PATH, LIST_10K_HASHED_PATH);
            //await GenerateHashedList.CreateHashesFileAsync_StreamReader_BinFile(LIST_ROCKYOU_PATH, LIST_ROCKYOU_HASHED_PATH);

            //await DictionaryFileIO.CreateDictionaryBinFileAsync_PlaintextKey(LIST_PATH_10K, DICTIONARY_BIN_PATH_10K);
            //await DictionaryFileIO.CreateDictionaryBinFileAsync_HashKey(LIST_PATH_10K, DICTIONARY_BIN_PATH_10K);
            await DictionaryFileIO.CreateDictionaryBinFileAsync_HashKey(LIST_PATH_ROCKYOU, DICTIONARY_BIN_PATH_ROCKYOU);
            //var dictionary = DictionaryFileIO.ReadDictionaryBinFile(DICTIONARY_BIN_PATH_10K);
        }

        public static void PrintMD5HashAsync()
        {
            string hash = CreateMD5Hash("password");
            Console.WriteLine(hash);
        }
    }
}