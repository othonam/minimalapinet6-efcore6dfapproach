using Microsoft.EntityFrameworkCore;
using Minimal.Domain.Entities;
using Minimal.Infrastructure.Contexts.Mappings;

namespace Minimal.Infraestructure.Context
{
    public class AppDbContext : DbContext
    {
        public DbSet<Costumer> Costumers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CustomerMap());
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                @"Server=(localdb)\mssqllocaldb;Database=Nome_BancoDados;Trusted_Connection=True;");
        }
    }
}
