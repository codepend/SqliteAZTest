using SharedLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Data
{
    //public class ApplicationDbContext : DbContext
    //{

    //    public DbSet<Person> People { get; set; }

    //    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    //    {

    //    }

    //    //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //    //{
    //    //    optionsBuilder.UseSqlite(
    //    //        @"Data Source=./data.db");
    //    //}
    //    protected override void OnModelCreating(ModelBuilder modelBuilder)
    //    {
    //        modelBuilder.Entity<Person>().HasData(
    //            new Person
    //            {
    //                Id = 1,
    //                FirstName = "John",
    //                LastName = "Doe",
    //                Age = 25,
    //                Email = "john.doe@nowhere.local"
    //            },
    //            new Person
    //            {
    //                Id = 2,
    //                FirstName = "Jane",
    //                LastName = "Doe",
    //                Age = 30,
    //                Email = "jane.doe@proggerssmells.nw"
    //            }
    //        );
    //    }

    //}

    //public class BloggingContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    //{
    //    public ApplicationDbContext CreateDbContext(string[] args)
    //    {
    //        var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
    //        optionsBuilder.UseSqlite("Data Source=data.db");

    //        return new ApplicationDbContext(optionsBuilder.Options);
    //    }
    //}
}
