﻿using BenchmarkDotNet;
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
        private readonly string readPath = LIST_PATH_10K;
        private readonly string writePath = HASHED_LIST_PATH_10K;

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