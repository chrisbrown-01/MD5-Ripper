using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Shared.FilePaths;

namespace Shared
{
    public class ReadFiles
    {
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
    }
}