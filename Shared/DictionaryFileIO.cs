using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO.Compression;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Shared
{
    public static class DictionaryFileIO
    {
        public static async Task CreateDictionaryBinFileAsync_PlaintextKey(string readPath, string writePath)
        {
            using var reader = new StreamReader(readPath);
            //using var outputFile = new BinaryWriter(File.Open(writePath, FileMode.Create));
            var dictionary = new Dictionary<string, string>();

            string? line;
            while ((line = await reader.ReadLineAsync()) != null)
            {
                var inputBytes = Encoding.UTF8.GetBytes(line);
                var hashBytes = MD5.HashData(inputBytes);
                //outputFile.Write(hashBytes);
                dictionary.Add(line, Convert.ToHexString(hashBytes));
            }
        }

        public static async Task CreateDictionaryBinFileAsync_HashKey(string readPath, string writePath)
        {
            using var reader = new StreamReader(readPath);
            var dictionary = new Dictionary<string, string>();

            string? line;
            while ((line = await reader.ReadLineAsync()) != null)
            {
                var inputBytes = Encoding.UTF8.GetBytes(line);
                var hashBytes = MD5.HashData(inputBytes);
                dictionary.TryAdd(Convert.ToHexString(hashBytes), line);
            }

            using var fileStream = new FileStream(writePath, FileMode.Create);
            using var writer = new StreamWriter(fileStream);
            string json = JsonSerializer.Serialize(dictionary);
            writer.Write(json);
        }

        public static async Task CreateDictionaryBinFileAsync_Compressed_HashKey(string readPath, string writePath)
        {
            using var reader = new StreamReader(readPath);
            var dictionary = new Dictionary<string, string>();

            string? line;
            while ((line = await reader.ReadLineAsync()) != null)
            {
                var inputBytes = Encoding.UTF8.GetBytes(line);
                var hashBytes = MD5.HashData(inputBytes);
                dictionary.TryAdd(Convert.ToHexString(hashBytes), line);
            }

            using var fileStream = new FileStream(writePath, FileMode.Create);
            using var compressionStream = new DeflateStream(fileStream, CompressionMode.Compress);
            using var writer = new StreamWriter(compressionStream);
            string json = JsonSerializer.Serialize(dictionary);
            writer.Write(json);
        }
    }
}