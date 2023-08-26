using BenchmarkDotNet;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using System;
using System.Threading.Tasks;
using static Shared.FilePaths;

// TODO: run benchmarking
namespace Benchmarking
{
    [MemoryDiagnoser]
    public class Benchmarks_RockYou
    {
        private readonly string readPath = LIST_ROCKYOU_PATH;
        private readonly string writePath = LIST_ROCKYOU_HASHED_PATH;

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
    }
}