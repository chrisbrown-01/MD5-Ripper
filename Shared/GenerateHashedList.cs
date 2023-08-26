using System.IO;
using System.Security.Cryptography;
using System.Text;
using static Shared.MD5_Helpers;

namespace Shared
{
    public static class GenerateHashedList
    {
        public static async Task CreateHashesFileAsync_StreamReader_Unoptimized(string path)
        {
            using var reader = new StreamReader(path);
            using var outputFile = new StreamWriter(path);

            string? line;
            while ((line = await reader.ReadLineAsync()) != null)
            {
                await outputFile.WriteLineAsync(CreateMD5Hash(line));
            }
        }

        public static async Task CreateHashesFileAsync_FileReadAllLinesIntoMemory_Unoptimized(string path)
        {
            var listArray = await File.ReadAllLinesAsync(path);
            using var outputFile = new StreamWriter(path);

            foreach (var item in listArray)
            {
                //if (item is null) continue;
                await outputFile.WriteLineAsync(CreateMD5Hash(item));
            }
        }

        public static async Task CreateHashesFileAsync_StreamReader(string path)
        {
            using var reader = new StreamReader(path);
            using var outputFile = new StreamWriter(path);
            using var md5 = MD5.Create();

            string? line;
            while ((line = await reader.ReadLineAsync()) != null)
            {
                var inputBytes = Encoding.UTF8.GetBytes(line);
                var hashBytes = md5.ComputeHash(inputBytes);
                await outputFile.WriteLineAsync(Convert.ToHexString(hashBytes));
            }
        }

        public static async Task CreateHashesFileAsync_FileReadAllLinesIntoMemory(string path)
        {
            var listArray = await File.ReadAllLinesAsync(path);
            using var outputFile = new StreamWriter(path);
            using var md5 = MD5.Create();

            foreach (var item in listArray)
            {
                //if (item is null) continue;
                var inputBytes = Encoding.UTF8.GetBytes(item);
                var hashBytes = md5.ComputeHash(inputBytes);
                await outputFile.WriteLineAsync(Convert.ToHexString(hashBytes));
            }
        }

        public static void CreateHashesFile_Parallel(string path) // TODO: note that lines may be out of order from password list
        {
            using var outputFile = new StreamWriter(path);
            var _lock = new SemaphoreSlim(1); // Limit the number of threads that can access the StreamWriter object

            Parallel.ForEach(File.ReadLines(path), async (line) =>
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