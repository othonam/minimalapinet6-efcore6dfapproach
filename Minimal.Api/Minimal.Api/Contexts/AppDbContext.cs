using Microsoft.EntityFrameworkCore;
using Minimal.Api.Configurations;
using Minimal.Api.Entities;

namespace Minimal.Api.Context
{
    //Using EFCore with Database-first approach
    public class AppDbContext : DbContext
    {
        private readonly IConfig _config;

        public AppDbContext(IConfig config) 
        {
            _config = config;
        }

        public DbSet<Customer> Customers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connString = _config.GetConnectionString();
            optionsBuilder.UseSqlServer(connString, builder => 
            {
                builder.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null);
            });
        }
    }
}
