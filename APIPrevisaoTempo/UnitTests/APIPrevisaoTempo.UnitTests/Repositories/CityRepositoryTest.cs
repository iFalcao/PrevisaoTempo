using APIPrevisaoTempo.WebApi.Data;
using APIPrevisaoTempo.WebApi.Data.Repositories;
using APIPrevisaoTempo.WebApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace APIPrevisaoTempo.UnitTests.Repositories
{
    public class CityRepositoryTest
    {
        private static DbContextOptions<DataContext> CreateNewContextOptions()
        {
            // Create a fresh service provider, and therefore a fresh 
            // InMemory database instance.
            var serviceProvider = new ServiceCollection()
                .AddEntityFrameworkInMemoryDatabase()
                .BuildServiceProvider();

            // Create a new options instance telling the context to use an
            // InMemory database and the new service provider.
            var builder = new DbContextOptionsBuilder<DataContext>();
            builder.UseInMemoryDatabase()
                   .UseInternalServiceProvider(serviceProvider);

            return builder.Options;
        }

        [Fact]
        public void AddACityAndReturnsItWithId()
        {
            using (var context = new DataContext(CreateNewContextOptions()))
            {
                CityRepository repository = new CityRepository(context);
                var insertedCity = repository.Insert(new City
                {
                    Name = "Salvador",
                    CustomCode = "12345",
                    Country = "BR",
                    Latitude = 254.234,
                    Longitude = 44.234
                });
                Assert.Equal(1, insertedCity.Id);
            }
        }

        [Fact]
        public void Add2CitiesAndRecover2CitiesFromMockedDb()
        {
            using (var context = new DataContext(CreateNewContextOptions()))
            {
                CityRepository repository = new CityRepository(context);
                repository.Insert(new City
                {
                    Name = "São Paulo",
                    CustomCode = "45674568",
                    Country = "BR",
                    Latitude = 24.234,
                    Longitude = 43.234
                });
                repository.Insert(new City
                {
                    Name = "Rio de Janeiro",
                    CustomCode = "234234",
                    Country = "BR",
                    Latitude = 2.234,
                    Longitude = 4.234
                });
                Assert.Equal(2, repository.SelectAll().Count());
            }
        }

        [Fact]
        public void DatabaseStartsEmpty()
        {
            using (var context = new DataContext(CreateNewContextOptions()))
            {
                CityRepository repository = new CityRepository(context);
                Assert.Empty(repository.SelectAll());
            }
        }

        [Fact]
        public void FindCorrectMatchesWithQuery()
        {
            using (var context = new DataContext(CreateNewContextOptions()))
            {
                CityRepository repository = new CityRepository(context);
                repository.Insert(new City
                {
                    Name = "São Paulo",
                    CustomCode = "45674568",
                    Country = "BR",
                    Latitude = 24.234,
                    Longitude = 43.234
                });
                repository.Insert(new City
                {
                    Name = "Rio de Janeiro",
                    CustomCode = "234234",
                    Country = "BR",
                    Latitude = 2.234,
                    Longitude = 4.234
                });

                Assert.Equal(1, repository.Where(q => q.Name == "São Paulo").Count());
                Assert.Equal(1, repository.Where(q => q.Id == 1).Count());
                Assert.Empty(repository.Where(q => q.Name == "Salvador"));
                Assert.Empty(repository.Where(q => q.Id < 1));
            }
        }
    }
}
