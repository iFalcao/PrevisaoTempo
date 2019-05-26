using APIPrevisaoTempo.WebApi.Data.Mapping;
using APIPrevisaoTempo.WebApi.Models;
using Microsoft.EntityFrameworkCore;

namespace APIPrevisaoTempo.WebApi.Data
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
