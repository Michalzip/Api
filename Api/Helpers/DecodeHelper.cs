using System;
using System.IO.Compression;
using System.Text;

namespace Api.Helpers
{
    public static class DecodeHelper
    {
        public static async Task<string> DecompressGZipStreamAsync(HttpResponseMessage response)
        {
            using Stream responseStream = await response.Content.ReadAsStreamAsync();
            using var decompressionStream = new GZipStream(
                responseStream,
                CompressionMode.Decompress
            );
            using var reader = new StreamReader(decompressionStream, Encoding.UTF8);
            return await reader.ReadToEndAsync();
        }
    }
}
