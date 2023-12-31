﻿using BenchmarkDotNet.Attributes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Shared.FilePaths;
using static Shared.PrintFiles;

namespace Benchmarking
{
    [MemoryDiagnoser]
    public class Benchmarks_PrintFiles
    {
        [Benchmark]
        public async Task PrintFileContentsToConsole_ReadAllLinesIntoMemoryAsync_10k()
        {
            await PrintFileContentsToConsole_ReadAllLinesIntoMemoryAsync(LIST_PATH_10K);
        }

        [Benchmark]
        public async Task PrintFileContentsToConsole_ReadAllLinesIntoMemoryAsync_RockYou()
        {
            await PrintFileContentsToConsole_ReadAllLinesIntoMemoryAsync(LIST_PATH_ROCKYOU);
        }

        [Benchmark]
        public async Task PrintFileContentsToConsole_ReadAllLinesAsStreamAsync_10k()
        {
            await PrintFileContentsToConsole_ReadAllLinesAsStreamAsync(LIST_PATH_10K);
        }

        [Benchmark]
        public async Task PrintFileContentsToConsole_ReadAllLinesAsStreamAsync_RockYou()
        {
            await PrintFileContentsToConsole_ReadAllLinesAsStreamAsync(LIST_PATH_ROCKYOU);
        }
    }
}