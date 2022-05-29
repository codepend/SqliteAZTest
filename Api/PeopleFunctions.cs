using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Microsoft.EntityFrameworkCore;
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
        private readonly IDbContextFactory<ApplicationDbContext> _dbContextFactory;

        public PeopleFunctions(IDbContextFactory<ApplicationDbContext> dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        [FunctionName("GetAllPeople")]
        public async Task<IActionResult> GetAllPeople(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            [Blob("data/data.json", FileAccess.Read, Connection = "DbStorage")] string peopleString,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            //using var context = _dbContextFactory.CreateDbContext();

            //await context.Database.EnsureCreatedAsync();

            //var people = await context.People.ToListAsync();

            //var json = JsonConvert.SerializeObject(people);

            //myBlob.Write(System.Text.Encoding.UTF8.GetBytes(json));

            var people = JsonConvert.DeserializeObject<List<Person>>(peopleString);
            
            return new OkObjectResult(people);
        }
        [FunctionName("AddPerson")]
        public async Task<IActionResult> AddPerson(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req,
            [Blob("data/data.json", FileAccess.Read, Connection = "DbStorage")] string peopleString,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");
            var body = await new StreamReader(req.Body).ReadToEndAsync();
            var person = JsonConvert.DeserializeObject<Person>(body);

            var fileName = "data.json";
            var containerName = "data";
                      
            var people = JsonConvert.DeserializeObject<List<Person>>(peopleString);
            people.Add(person);

            await BlobHelper.WriteListAsJsonToBlob(fileName, containerName, people);

            return new OkObjectResult(person);
        }
    }
}
