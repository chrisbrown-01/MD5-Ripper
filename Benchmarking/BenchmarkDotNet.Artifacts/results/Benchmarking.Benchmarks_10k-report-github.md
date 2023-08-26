```

BenchmarkDotNet v0.13.7, Windows 10 (10.0.19045.3324/22H2/2022Update)
Intel Core i7-1065G7 CPU 1.30GHz, 1 CPU, 8 logical and 4 physical cores
.NET SDK 7.0.306
  [Host]     : .NET 7.0.9 (7.0.923.32018), X64 RyuJIT AVX2
  DefaultJob : .NET 7.0.9 (7.0.923.32018), X64 RyuJIT AVX2


```
|                                                       Method |      Mean |     Error |    StdDev |      Gen0 |     Gen1 |    Gen2 | Allocated |
|------------------------------------------------------------- |----------:|----------:|----------:|----------:|---------:|--------:|----------:|
|                           CreateHashesFileAsync_StreamReader |  9.757 ms | 0.1950 ms | 0.4520 ms |  656.2500 |        - |       - |   2.64 MB |
|               CreateHashesFileAsync_StreamReader_Unoptimized | 13.605 ms | 0.1636 ms | 0.1450 ms | 1062.5000 |        - |       - |   4.24 MB |
|             CreateHashesFileAsync_FileReadAllLinesIntoMemory | 11.685 ms | 0.2014 ms | 0.1682 ms |  484.3750 | 125.0000 | 62.5000 |   2.97 MB |
| CreateHashesFileAsync_FileReadAllLinesIntoMemory_Unoptimized | 15.421 ms | 0.2161 ms | 0.1916 ms |  750.0000 | 203.1250 | 93.7500 |   4.57 MB |
|                                    CreateHashesFile_Parallel |        NA |        NA |        NA |        NA |       NA |      NA |        NA |

Benchmarks with issues:
  Benchmarks_10k.CreateHashesFile_Parallel: DefaultJob
