using APIPrevisaoTempo.Infra.Data.Mapping;
using APIPrevisaoTempo.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace APIPrevisaoTempo.Infra.Data.Context
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<City> Cities { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new CityConfig());
        }
    }
}
