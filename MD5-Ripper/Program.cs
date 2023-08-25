using System.Security.Cryptography;
using System.Text;

namespace MD5_Ripper
{
    // TODO: detect if hash is MD5 feature
    // TODO: benchmark.net
    // TODO: precomputed hashtable/dictionary of rockyou, stored in file or in app?
    internal class Program
    {
        public const string LIST_10K_PATH = "C:\\Users\\chris\\Downloads\\10k-most-common.txt";
        public const string PRINT_SINGLE_MD5_HASH_PATH = "..\\..\\..\\PrintMD5HashAsync.txt";
        public const string LIST_10K_HASHED_PATH = "..\\..\\..\\10k-most-common-hashed.txt";
        public const string LIST_ROCKYOU_PATH = "C:\\Users\\chris\\Downloads\\rockyou.txt";
        public const string LIST_ROCKYOU_HASHED_PATH = "C:\\Users\\chris\\Downloads\\rockyou-hashed.txt";

        public static string CreateMD5Hash(string input)
        {
            using System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create();

            byte[] inputBytes = System.Text.Encoding.UTF8.GetBytes(input);
            byte[] hashBytes = md5.ComputeHash(inputBytes);

            return Convert.ToHexString(hashBytes);
        }

        public static async Task ReadAllLinesIntoMemoryAsync()
        {
            Console.WriteLine("");
            var listArray = await File.ReadAllLinesAsync(LIST_10K_PATH);
            //var list = listArray.ToList();
            //foreach (var item in list)
            //{
            //    Console.WriteLine(item);
            //}
            Console.WriteLine("");
        }

        public static async Task ReadAllLinesAsStreamAsync()
        {
            Console.WriteLine("");
            using var reader = new StreamReader(LIST_10K_PATH);

            string? line;
            while ((line = await reader.ReadLineAsync()) != null)
            {
                Console.WriteLine(line);
            }

            Console.WriteLine("");
        }

        public static async Task Create10kHashesFileAsync_Unoptimized()
        {
            Console.WriteLine("");
            using var reader = new StreamReader(LIST_10K_PATH);
            using var outputFile = new StreamWriter(LIST_10K_HASHED_PATH);

            string? line;
            while ((line = await reader.ReadLineAsync()) != null)
            {
                await outputFile.WriteLineAsync(CreateMD5Hash(line));
            }

            Console.WriteLine("");
        }

        public static async Task Create10kHashesFileAsync_SingleThreaded()
        {
            Console.WriteLine("");
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

            Console.WriteLine("");
        }

        public static async Task CreateRockYouHashesFileAsync_Unoptimized()
        {
            Console.WriteLine("");
            using var reader = new StreamReader(LIST_ROCKYOU_PATH);
            using var outputFile = new StreamWriter(LIST_ROCKYOU_HASHED_PATH);

            string? line;
            while ((line = await reader.ReadLineAsync()) != null)
            {
                await outputFile.WriteLineAsync(CreateMD5Hash(line));
            }

            Console.WriteLine("");
        }

        public static async Task CreateRockYouHashesFileAsync_SingleThreaded()
        {
            Console.WriteLine("");
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

            Console.WriteLine("");
        }

        public static void Create10kHashesFile_Parallel() // TODO: note that lines may be out of order from password list
        {
            Console.WriteLine("");
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

            Console.WriteLine("");
        }

        public static void CreateRockYouHashesFile_Parallel()
        {
            // Beyond horrible performance. Not even worth trying to run this to completion.

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
        }

        public static async Task PrintMD5HashAsync()
        {
            string hash = CreateMD5Hash("password");

            using StreamWriter outputFile = new StreamWriter(PRINT_SINGLE_MD5_HASH_PATH);
            await outputFile.WriteLineAsync(hash);
        }

        static async Task Main(string[] args)
        {
            //await PrintMD5HashAsync();
            //await ReadAllLinesIntoMemoryAsync();
            //await ReadAllLinesAsStreamAsync();
            //await Create10kHashesFileAsync_Unoptimized();
            //Create10kHashesFile_Parallel();

            await Create10kHashesFileAsync_SingleThreaded();

            //await CreateRockYouHashesFileAsync_SingleThreaded();
            //await CreateRockYouHashesFileAsync_Unoptimized();
            //CreateRockYouHashesFile_Parallel();
        }
    }
}