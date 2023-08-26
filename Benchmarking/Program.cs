using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Running;

namespace Benchmarking
{
    public class Program
    {
        // cd C:\Users\chris\Documents\GitHub\MD5-Ripper\Benchmarking
        // dotnet run --project Benchmarking.csproj -c Release
        public static void Main(string[] args)
        {
            var config = DefaultConfig.Instance;
            //var summary = BenchmarkRunner.Run<Benchmarks_10k>(config, args);

            // Use this to select benchmarks from the console:
            var summaries = BenchmarkSwitcher.FromAssembly(typeof(Program).Assembly).Run(args, config);
        }
    }
}