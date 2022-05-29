using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Helpers
{
    public class BlobHelper
    {
        public static async Task WriteListAsJsonToBlob<T>(string fileName, string containerName, List<T> list) 
        {
            var blobClient = GetBlobClient(fileName, containerName);
            var json = JsonConvert.SerializeObject(list);
            var bytes = System.Text.Encoding.UTF8.GetBytes(json);
            var stream = new MemoryStream(bytes);
            await blobClient.UploadAsync(stream, true);
        }

        public static async Task<List<T>> GetJsonListFromBlob<T>(string fileName, string containerName)
        {
            var output = await DownloadFileAsString(fileName, containerName);
            return JsonConvert.DeserializeObject<List<T>>(output);
        }

        public static async Task<string> DownloadFileAsString(string fileName, string containerName)
        {
            var blobClient = GetBlobClient(fileName, containerName);
            if (await blobClient.ExistsAsync())
            {
                BlobDownloadInfo download = await blobClient.DownloadAsync();
                byte[] result = new byte[download.ContentLength];
                await download.Content.ReadAsync(result, 0, (int)download.ContentLength);

                return Encoding.UTF8.GetString(result);
            }
            return string.Empty;
        }

        private static BlobClient GetBlobClient(string fileName, string containerName)
        {
            var connectionString = Environment.GetEnvironmentVariable("DbStorage");
            BlobServiceClient blobServiceClient = new BlobServiceClient(connectionString);
            BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient(containerName);
            BlobClient blobClient = containerClient.GetBlobClient(fileName);
            return blobClient;
        }

        
    }
}
