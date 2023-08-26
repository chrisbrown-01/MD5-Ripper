# MD5-Ripper

This project calculates MD5 hashes of the rockyou and 10k-most-common password wordlists. 
This was written to experiment with performance improvements when comparing files being loaded into memory vs. streaming them in C#. 
See BenchmarkDotNet results below:

### Benchmarks_10k ###

|                                                       Method |      Mean |     Error |    StdDev |      Gen0 |     Gen1 |    Gen2 | Allocated |
|------------------------------------------------------------- |----------:|----------:|----------:|----------:|---------:|--------:|----------:|
|                           CreateHashesFileAsync_StreamReader |  9.757 ms | 0.1950 ms | 0.4520 ms |  656.2500 |        - |       - |   2.64 MB |
|               CreateHashesFileAsync_StreamReader_Unoptimized | 13.605 ms | 0.1636 ms | 0.1450 ms | 1062.5000 |        - |       - |   4.24 MB |
|             CreateHashesFileAsync_FileReadAllLinesIntoMemory | 11.685 ms | 0.2014 ms | 0.1682 ms |  484.3750 | 125.0000 | 62.5000 |   2.97 MB |
| CreateHashesFileAsync_FileReadAllLinesIntoMemory_Unoptimized | 15.421 ms | 0.2161 ms | 0.1916 ms |  750.0000 | 203.1250 | 93.7500 |   4.57 MB |
|                                    CreateHashesFile_Parallel |        NA |        NA |        NA |        NA |       NA |      NA |        NA |



### Benchmarks_PrintFiles ###

|                                                         Method |         Mean |       Error |      StdDev |       Median |        Gen0 |       Gen1 |      Gen2 |  Allocated |
|--------------------------------------------------------------- |-------------:|------------:|------------:|-------------:|------------:|-----------:|----------:|-----------:|
|     PrintFileContentsToConsole_ReadAllLinesIntoMemoryAsync_10k |     2.824 ms |   0.0561 ms |   0.1573 ms |     2.869 ms |    218.7500 |   128.9063 |   62.5000 |     1.4 MB |
| PrintFileContentsToConsole_ReadAllLinesIntoMemoryAsync_RockYou | 5,075.651 ms | 160.3672 ms | 472.8463 ms | 4,894.969 ms | 275000.0000 | 96000.0000 | 7000.0000 | 1972.22 MB |
|       PrintFileContentsToConsole_ReadAllLinesAsStreamAsync_10k |     1.238 ms |   0.0602 ms |   0.1767 ms |     1.189 ms |    269.5313 |          - |         - |    1.08 MB |
|   PrintFileContentsToConsole_ReadAllLinesAsStreamAsync_RockYou | 1,562.837 ms |  15.3794 ms |  13.6334 ms | 1,561.141 ms | 403000.0000 |          - |         - | 1606.77 MB |