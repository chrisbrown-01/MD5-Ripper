```

BenchmarkDotNet v0.13.7, Windows 10 (10.0.19045.3324/22H2/2022Update)
Intel Core i7-1065G7 CPU 1.30GHz, 1 CPU, 8 logical and 4 physical cores
.NET SDK 7.0.306
  [Host]     : .NET 7.0.9 (7.0.923.32018), X64 RyuJIT AVX2
  DefaultJob : .NET 7.0.9 (7.0.923.32018), X64 RyuJIT AVX2


```
|                                                                Method |      Mean |     Error |    StdDev |    Median |      Gen0 | Allocated |
|---------------------------------------------------------------------- |----------:|----------:|----------:|----------:|----------:|----------:|
|                    GenerateAllPasswordsOfLength_MultiThreaded_Length3 |  5.513 ms | 0.1621 ms | 0.4678 ms |  5.724 ms | 6437.5000 |  25.64 MB |
|  GenerateAllPasswordsOfLength_MultiThreaded_WithStringBuilder_Length3 |  7.067 ms | 0.1398 ms | 0.1373 ms |  7.034 ms | 6445.3125 |  25.65 MB |
| GenerateAllPasswordsOfLength_SingleThreaded_WithStringBuilder_Length3 | 11.576 ms | 0.2272 ms | 0.4039 ms | 11.677 ms | 6421.8750 |  25.62 MB |
|                   GenerateAllPasswordsOfLength_SingleThreaded_Length3 |  8.343 ms | 0.1648 ms | 0.3136 ms |  8.284 ms | 6421.8750 |  25.62 MB |
