using BenchmarkDotNet;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using System;
using System.Threading.Tasks;
using static Shared.FilePaths;

namespace Benchmarking
{
    [MemoryDiagnoser]
    public class Benchmarks_RockYou
    {
        private string path = LIST_ROCKYOU_PATH;

        [Benchmark]
        public async Task CreateHashesFileAsync_StreamReader()
        {
            await Shared.GenerateHashedList.CreateHashesFileAsync_StreamReader(path);
        }

        [Benchmark]
        public async Task CreateHashesFileAsync_StreamReader_Unoptimized()
        {
            await Shared.GenerateHashedList.CreateHashesFileAsync_StreamReader_Unoptimized(path);
        }

        [Benchmark]
        public async Task CreateHashesFileAsync_FileReadAllLinesIntoMemory()
        {
            await Shared.GenerateHashedList.CreateHashesFileAsync_FileReadAllLinesIntoMemory(path);
        }

        [Benchmark]
        public async Task CreateHashesFileAsync_FileReadAllLinesIntoMemory_Unoptimized()
        {
            await Shared.GenerateHashedList.CreateHashesFileAsync_FileReadAllLinesIntoMemory_Unoptimized(path);
        }
    }
}