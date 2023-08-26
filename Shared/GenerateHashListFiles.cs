using System.Security.Cryptography;
using System.Text;

namespace Shared
{
    public static class GenerateHashListFiles
    {
        public const string LIST_10K_PATH = "C:\\Users\\chris\\Downloads\\10k-most-common.txt";
        public const string PRINT_SINGLE_MD5_HASH_PATH = "..\\..\\..\\PrintMD5HashAsync.txt";
        public const string LIST_10K_HASHED_PATH = "..\\..\\..\\10k-most-common-hashed.txt";
        public const string LIST_ROCKYOU_PATH = "C:\\Users\\chris\\Downloads\\rockyou.txt";
        public const string LIST_ROCKYOU_HASHED_PATH = "C:\\Users\\chris\\Downloads\\rockyou-hashed.txt";

        public static string CreateMD5Hash(string input)
        {
            using MD5 md5 = MD5.Create();

            byte[] inputBytes = Encoding.UTF8.GetBytes(input);
            byte[] hashBytes = md5.ComputeHash(inputBytes);

            return Convert.ToHexString(hashBytes);
        }

        public static async Task ReadAllLinesIntoMemoryAsync()
        {
            var listArray = await File.ReadAllLinesAsync(LIST_10K_PATH);
            //var list = listArray.ToList();
            //foreach (var item in list)
            //{
            //    Console.WriteLine(item);
            //}
        }

        public static async Task ReadAllLinesAsStreamAsync()
        {
            using var reader = new StreamReader(LIST_10K_PATH);

            string? line;
            while ((line = await reader.ReadLineAsync()) != null)
            {
                Console.WriteLine(line);
            }
        }

        public static async Task Create10kHashesFileAsync_Unoptimized()
        {
            using var reader = new StreamReader(LIST_10K_PATH);
            using var outputFile = new StreamWriter(LIST_10K_HASHED_PATH);

            string? line;
            while ((line = await reader.ReadLineAsync()) != null)
            {
                await outputFile.WriteLineAsync(CreateMD5Hash(line));
            }
        }

        public static async Task Create10kHashesFileAsync_SingleThreaded()
        {
            using var reader = new StreamReader(LIST_10K_PATH);
            using var outputFile = new StreamWriter(LIST_10K_HASHED_PATH);
            using var md5 = MD5.Create();

            string? line;
            while ((line = await reader.ReadLineAsync()) != null)
            {
                var inputBytes = Encoding.UTF8.GetBytes(line);
                var hashBytes = md5.ComputeHash(inputBytes);
                await outputFile.WriteLineAsync(Convert.ToHexString(hashBytes));
            }
        }

        public static async Task CreateRockYouHashesFileAsync_Unoptimized()
        {
            using var reader = new StreamReader(LIST_ROCKYOU_PATH);
            using var outputFile = new StreamWriter(LIST_ROCKYOU_HASHED_PATH);

            string? line;
            while ((line = await reader.ReadLineAsync()) != null)
            {
                await outputFile.WriteLineAsync(CreateMD5Hash(line));
            }
        }

        public static async Task CreateRockYouHashesFileAsync_SingleThreaded()
        {
            using var reader = new StreamReader(LIST_ROCKYOU_PATH);
            using var outputFile = new StreamWriter(LIST_ROCKYOU_HASHED_PATH);
            using var md5 = MD5.Create();

            string? line;
            while ((line = await reader.ReadLineAsync()) != null)
            {
                var inputBytes = Encoding.UTF8.GetBytes(line);
                var hashBytes = md5.ComputeHash(inputBytes);
                await outputFile.WriteLineAsync(Convert.ToHexString(hashBytes));
            }
        }

        public static void Create10kHashesFile_Parallel() // TODO: note that lines may be out of order from password list
        {
            using var outputFile = new StreamWriter(LIST_10K_HASHED_PATH);
            var _lock = new SemaphoreSlim(1); // Limit the number of threads that can access the StreamWriter object

            Parallel.ForEach(File.ReadLines(LIST_10K_PATH), async (line) =>
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

        // --- Beyond horrible performance. Not even worth trying to run this to completion. ---
        //public static void CreateRockYouHashesFile_Parallel()
        //{
        //Console.WriteLine("");
        //using var outputFile = new StreamWriter(LIST_ROCKYOU_HASHED_PATH);
        //var _lock = new SemaphoreSlim(1); // Limit the number of threads that can access the StreamWriter object

        //Parallel.ForEach(File.ReadLines(LIST_ROCKYOU_PATH), async (line) =>
        //{
        //    var hash = CreateMD5Hash(line);
        //    await _lock.WaitAsync();

        //    try
        //    {
        //        await outputFile.WriteLineAsync(hash);
        //    }
        //    finally
        //    {
        //        _lock.Release();
        //    }
        //});

        //Console.WriteLine("");
        //}
    }
}