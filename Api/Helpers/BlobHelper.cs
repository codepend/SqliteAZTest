using Azure.Storage.Blobs;
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
            var connectionString = Environment.GetEnvironmentVariable("DbStorage");
            BlobServiceClient blobServiceClient = new BlobServiceClient(connectionString);
            BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient(containerName);
            BlobClient blobClient = containerClient.GetBlobClient(fileName);
            var json = JsonConvert.SerializeObject(list);
            var bytes = System.Text.Encoding.UTF8.GetBytes(json);
            var stream = new MemoryStream(bytes);
            await blobClient.UploadAsync(stream, true);
        }
    }
}
