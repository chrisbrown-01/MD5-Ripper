```

BenchmarkDotNet v0.13.7, Windows 10 (10.0.19045.3324/22H2/2022Update)
Intel Core i7-1065G7 CPU 1.30GHz, 1 CPU, 8 logical and 4 physical cores
.NET SDK 7.0.306
  [Host]     : .NET 7.0.9 (7.0.923.32018), X64 RyuJIT AVX2
  DefaultJob : .NET 7.0.9 (7.0.923.32018), X64 RyuJIT AVX2


```
|                                                         Method |         Mean |       Error |      StdDev |       Median |        Gen0 |       Gen1 |      Gen2 |  Allocated |
|--------------------------------------------------------------- |-------------:|------------:|------------:|-------------:|------------:|-----------:|----------:|-----------:|
|     PrintFileContentsToConsole_ReadAllLinesIntoMemoryAsync_10k |     2.824 ms |   0.0561 ms |   0.1573 ms |     2.869 ms |    218.7500 |   128.9063 |   62.5000 |     1.4 MB |
| PrintFileContentsToConsole_ReadAllLinesIntoMemoryAsync_RockYou | 5,075.651 ms | 160.3672 ms | 472.8463 ms | 4,894.969 ms | 275000.0000 | 96000.0000 | 7000.0000 | 1972.22 MB |
|       PrintFileContentsToConsole_ReadAllLinesAsStreamAsync_10k |     1.238 ms |   0.0602 ms |   0.1767 ms |     1.189 ms |    269.5313 |          - |         - |    1.08 MB |
|   PrintFileContentsToConsole_ReadAllLinesAsStreamAsync_RockYou | 1,562.837 ms |  15.3794 ms |  13.6334 ms | 1,561.141 ms | 403000.0000 |          - |         - | 1606.77 MB |
