using BenchmarkDotNet;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using System;
using System.Threading.Tasks;
using static Shared.FilePaths;

namespace Benchmarking
{
    [MemoryDiagnoser]
    public class Benchmarks_10k
    {
        private string readPath = LIST_10K_PATH;
        private string writePath = LIST_10K_HASHED_PATH;

        [Benchmark]
        public async Task CreateHashesFileAsync_StreamReader()
        {
            await Shared.GenerateHashedList.CreateHashesFileAsync_StreamReader(readPath, writePath);
        }

        [Benchmark]
        public async Task CreateHashesFileAsync_StreamReader_Unoptimized()
        {
            await Shared.GenerateHashedList.CreateHashesFileAsync_StreamReader_Unoptimized(readPath, writePath);
        }

        [Benchmark]
        public async Task CreateHashesFileAsync_FileReadAllLinesIntoMemory()
        {
            await Shared.GenerateHashedList.CreateHashesFileAsync_FileReadAllLinesIntoMemory(readPath, writePath);
        }

        [Benchmark]
        public async Task CreateHashesFileAsync_FileReadAllLinesIntoMemory_Unoptimized()
        {
            await Shared.GenerateHashedList.CreateHashesFileAsync_FileReadAllLinesIntoMemory_Unoptimized(readPath, writePath);
        }

        [Benchmark]
        public void CreateHashesFile_Parallel()
        {
            Shared.GenerateHashedList.CreateHashesFile_Parallel(readPath, writePath);
        }
    }
}