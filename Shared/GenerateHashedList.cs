using System.IO;
using System.Security.Cryptography;
using System.Text;
using static Shared.Utility;

namespace Shared
{
    public static class GenerateHashedList
    {
        public static async Task CreateHashesFileAsync_StreamReader_Unoptimized(string readPath, string writePath)
        {
            using var reader = new StreamReader(readPath);
            using var outputFile = new StreamWriter(writePath);

            string? line;
            while ((line = await reader.ReadLineAsync()) != null)
            {
                await outputFile.WriteLineAsync(CreateMD5Hash(line));
            }
        }

        public static async Task CreateHashesFileAsync_FileReadAllLinesIntoMemory_Unoptimized(string readPath, string writePath)
        {
            var listArray = await File.ReadAllLinesAsync(readPath);
            using var outputFile = new StreamWriter(writePath);

            foreach (var item in listArray)
            {
                await outputFile.WriteLineAsync(CreateMD5Hash(item));
            }
        }

        public static async Task CreateHashesFileAsync_StreamReader(string readPath, string writePath)
        {
            using var reader = new StreamReader(readPath);
            using var outputFile = new StreamWriter(writePath);

            string? line;
            while ((line = await reader.ReadLineAsync()) != null)
            {
                var inputBytes = Encoding.UTF8.GetBytes(line);
                var hashBytes = MD5.HashData(inputBytes);
                await outputFile.WriteLineAsync(Convert.ToHexString(hashBytes));
            }
        }

        public static async Task CreateHashesFileAsync_FileReadAllLinesIntoMemory(string readPath, string writePath)
        {
            var listArray = await File.ReadAllLinesAsync(readPath);
            using var outputFile = new StreamWriter(writePath);

            foreach (var item in listArray)
            {
                var inputBytes = Encoding.UTF8.GetBytes(item);
                var hashBytes = MD5.HashData(inputBytes);
                await outputFile.WriteLineAsync(Convert.ToHexString(hashBytes));
            }
        }

        public static void CreateHashesFile_Parallel(string readPath, string writePath) // Note that lines may be out of order from password list
        {
            using var outputFile = new StreamWriter(writePath);
            var _lock = new SemaphoreSlim(1); // Limit the number of threads that can access the StreamWriter object

            Parallel.ForEach(File.ReadLines(readPath), async (line) =>
            {
                var hash = CreateMD5Hash(line);
                await _lock.WaitAsync();

                try
                {
                    await outputFile.WriteLineAsync(hash);
                }
                finally
                {
                    _lock.Release();
                }
            });
        }
    }
}