using BenchmarkDotNet;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using System;
using System.Threading.Tasks;

namespace Benchmarking
{
    public class Benchmarks
    {
        [Benchmark]
        public void Scenario1()
        {
            var a = 1;
            var b = 2;
            var c = a + b;
        }

        [Benchmark]
        public async Task Scenario2()
        {
            await Task.Delay(300);
            var a = 1;
            var b = 2;
            var c = a + b;
        }
    }
}