using Api.Data;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;

[assembly: FunctionsStartup(typeof(MyNamespace.Startup))]

namespace MyNamespace
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            var home = Environment.GetEnvironmentVariable("HOME") ?? "";
            
            //var home = Environment.GetEnvironmentVariable("HOME");
            var databasePath = Path.Combine(home, "data.db");

            builder.Services.AddDbContextFactory<ApplicationDbContext>(options =>
            {
                options.UseSqlite($"Data Source={databasePath}");
            });

        }
    }
}