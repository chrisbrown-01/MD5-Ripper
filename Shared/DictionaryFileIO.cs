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
                dictionary.Add(Convert.ToHexString(hashBytes), line);
            }

            //using var writer = new BinaryWriter(File.Open(writePath, FileMode.Create));
            using var writer = new DeflateStream(File.Open(writePath, FileMode.Create), CompressionMode.Compress);
            foreach (var pair in dictionary)
            {
                //writer.Write(pair.Key);
                //writer.Write(pair.Value);

                writer.Write(Encoding.ASCII.GetBytes(pair.Key), 0, Encoding.ASCII.GetBytes(pair.Key).Length);
                writer.Write(Encoding.ASCII.GetBytes(pair.Value), 0, Encoding.ASCII.GetBytes(pair.Value).Length);
            }
        }
    }
}