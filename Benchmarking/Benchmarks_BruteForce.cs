using BenchmarkDotNet.Attributes;
using static Shared.BruteForce;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Benchmarking
{
    [MemoryDiagnoser]
    public class Benchmarks_BruteForce
    {
        // Multithreaded without stringbuilder should be best

        [Benchmark]
        public void GenerateAllPasswordsOfLength_MultiThreaded_Length3()
        {
            GenerateAllPasswordsOfLength_MultiThreaded(3);
        }

        [Benchmark]
        public void GenerateAllPasswordsOfLength_MultiThreaded_WithStringBuilder_Length3()
        {
            GenerateAllPasswordsOfLength_MultiThreaded_WithStringBuilder(3);
        }

        [Benchmark]
        public void GenerateAllPasswordsOfLength_SingleThreaded_WithStringBuilder_Length3()
        {
            GenerateAllPasswordsOfLength_SingleThreaded_WithStringBuilder(3);
        }

        [Benchmark]
        public void GenerateAllPasswordsOfLength_SingleThreaded_Length3()
        {
            GenerateAllPasswordsOfLength_SingleThreaded(3);
        }
    }
}