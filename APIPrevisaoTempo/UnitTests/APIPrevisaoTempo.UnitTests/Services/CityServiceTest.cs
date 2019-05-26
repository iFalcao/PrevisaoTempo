using APIPrevisaoTempo.Common.Objects;
using APIPrevisaoTempo.External.OpenWeatherProxy.Services;
using APIPrevisaoTempo.WebApi.Data.Repositories;
using APIPrevisaoTempo.WebApi.Models;
using APIPrevisaoTempo.WebApi.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace APIPrevisaoTempo.UnitTests.Services
{
    public class CityServiceTest : IClassFixture<ConfigFixture>
    {
        private readonly Mock<ICityRepository> _cityRepository;
        private readonly Mock<IExternalCityService> _externalService;
        private readonly City _cityMock = new City
        {
            Name = "Salvador",
            CustomCode = "43534",
            Country = "BR",
            Latitude = 975.435,
            Longitude = 975.435,
            Id = 1
        };

        public CityServiceTest(ConfigFixture fixture)
        {
            this._cityRepository = new Mock<ICityRepository>();
            this._externalService = new Mock<IExternalCityService>();
        }

        [Fact]
        public void RetrieveAllCities_WhenCalled_ReturnsAListOfCities()
        {
            // Arrange
            this._cityRepository.Setup(svc => svc.SelectAll()).Returns(new List<City>
            {
                _cityMock
            });

            // Act
            var listReturned = this.GenerateCityService().RetrieveAllCities();

            // Assert
            Assert.IsType<List<City>>(listReturned);
            Assert.True(listReturned.Count == 1);
        }

        [Fact]
        public void CreateCity_WhenCalled_ReturnsTheInsertedCity()
        {
            // Arrange
            this._cityRepository.Setup(svc => svc.Insert(It.IsAny<City>())).Returns(new City
            {
                Id = 1
            });
            
            // usado para verificar existência de cidades cadastradas com o mesmo custom code e portanto deve retornar 0
            this._cityRepository.Setup(svc => svc
                .Where(It.IsAny<System.Linq.Expressions.Expression<System.Func<City, bool>>>()))
                .Returns(new List<City>().AsQueryable()); 

            // usado para verificar se existe cidade com esse nome na api externa
            this._externalService.Setup(svc => svc.SearchCitiesByName(It.IsAny<string>())).Returns(new FoundCitiesDTO
            {
                count = 1
            });


            // Act
            var cityInserted = this.GenerateCityService().CreateCity(new City
            {
                Name = "Salvador",
                CustomCode = "43534",
                Country = "BR",
                Latitude = 975.435,
                Longitude = 975.435
            });

            // Assert
            Assert.IsType<City>(cityInserted);
            Assert.Equal(1, cityInserted.Id);
        }

        [Fact]
        public void CreateCity_WhenCalledAndCityDoesntExistOnExternalApi_ReturnsException()
        {
            // Arrange
            // usado para verificar se existe cidade com esse nome na api externa, se retornado 0 não deve permitir cadastro
            this._externalService.Setup(svc => svc.SearchCitiesByName(It.IsAny<string>())).Returns(new FoundCitiesDTO
            {
                count = 0
            });

            // Act
            var exception = Record.Exception(() => 
                this.GenerateCityService().CreateCity(new City
                {
                    Name = "Salvador",
                    CustomCode = "43534",
                    Country = "BR",
                    Latitude = 975.435,
                    Longitude = 975.435
                })
            );

            // Assert
            Assert.NotNull(exception);
            Assert.IsType<ArgumentException>(exception);
        }

        [Fact]
        public void CreateCity_WhenCalledAndCityAlreadyExistsOnDb_ReturnsException()
        {
            // Arrange
            // Cidade existe na api externa
            this._externalService.Setup(svc => svc.SearchCitiesByName(It.IsAny<string>())).Returns(new FoundCitiesDTO
            {
                count = 1
            });

            // Nesse teste, o repositório retorna uma cidade cadastrada e deve retornar erro
            this._cityRepository.Setup(svc => svc
                .Where(It.IsAny<System.Linq.Expressions.Expression<System.Func<City, bool>>>()))
                .Returns(new List<City> { _cityMock }.AsQueryable());

            // Act
            var exception = Record.Exception(() =>
                this.GenerateCityService().CreateCity(new City
                {
                    Name = "Salvador",
                    CustomCode = "43534",
                    Country = "BR",
                    Latitude = 975.435,
                    Longitude = 975.435
                })
            );

            // Assert
            Assert.NotNull(exception);
            Assert.IsType<ArgumentException>(exception);
        }


        private CityService GenerateCityService()
        {
            return new CityService(_cityRepository.Object, _externalService.Object);
        }
    }
}
