using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Shared.FilePaths;

namespace Shared
{
    public class PrintFiles
    {
        public static async Task PrintFileContentsToConsole_ReadAllLinesIntoMemoryAsync(string path)
        {
            var array = await File.ReadAllLinesAsync(path);
            foreach (var item in array)
            {
                Console.WriteLine(item);
            }
        }

        public static async Task PrintFileContentsToConsole_ReadAllLinesAsStreamAsync(string path)
        {
            using var reader = new StreamReader(path);

            string? item;
            while ((item = await reader.ReadLineAsync()) != null)
            {
                Console.WriteLine(item);
            }
        }
    }
}