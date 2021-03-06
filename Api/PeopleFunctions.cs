using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Api.Data;
using SharedLibrary;
using System.Collections.Generic;
using Microsoft.Azure.Management.Storage.Models;
using Azure.Storage.Blobs;
using Api.Helpers;

namespace Api
{
    
    public class PeopleFunctions
    {
        private readonly string _containerName = "data";
        private readonly string _fileName = "data.json";
        private readonly BlobHelper _blobHelper;

        public PeopleFunctions(BlobHelper blobHelper)
        {
            _blobHelper = blobHelper;
        }

        [FunctionName("GetAllPeople")]
        public async Task<IActionResult> GetAllPeople(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            var people = await _blobHelper.GetJsonListFromBlob<Person>(_fileName, _containerName);
            
            return new OkObjectResult(people);
        }
        [FunctionName("AddPerson")]
        public async Task<IActionResult> AddPerson(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");
            var body = await new StreamReader(req.Body).ReadToEndAsync();
            var newPerson = JsonConvert.DeserializeObject<Person>(body);

            var people = await _blobHelper.GetJsonListFromBlob<Person>(_fileName, _containerName);
            people.Add(newPerson);

            await _blobHelper.WriteListAsJsonToBlob(_fileName, _containerName, people);

            return new OkObjectResult(newPerson);
        }
    }
}
