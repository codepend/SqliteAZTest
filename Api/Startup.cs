using Api.Data;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

[assembly: FunctionsStartup(typeof(MyNamespace.Startup))]

namespace MyNamespace
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            //builder.Services.AddDbContext<ApplicationDbContext>(options =>
            //{
            //    options.UseSqlite("Data Source=./data.db",
            //        b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName));
            //});


            builder.Services.AddDbContextFactory<ApplicationDbContext>(options =>
            {
                options.UseSqlite("Data Source=data.db");
            });

        }
    }
}