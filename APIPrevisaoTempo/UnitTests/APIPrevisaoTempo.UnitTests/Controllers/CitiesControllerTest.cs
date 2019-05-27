using APIPrevisaoTempo.Application.Services;
using APIPrevisaoTempo.Infra.CrossCutting.Objects;
using APIPrevisaoTempo.Domain.Models;
using APIPrevisaoTempo.Infra.CrossCutting.OpenWeatherProxy.Services;
using APIPrevisaoTempo.WebApi.Controllers;
using APIPrevisaoTempo.WebApi.DTOs;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Xunit;

namespace APIPrevisaoTempo.UnitTests.Controllers
{
    public class CitiesControllerTest : IClassFixture<ConfigFixture>
    {
        private readonly ServiceProvider _serviceProvider;
        private readonly Mock<ICityService> _cityServiceMock;
        private readonly Mock<IExternalCityService> _externalServiceMock;
        private readonly City _cityMock = new City
        {
            Name = "Salvador",
            CustomCode = "43534",
            Country = "BR",
            Latitude = 975.435,
            Longitude = 975.435,
            Id = 1
        };

        public CitiesControllerTest(ConfigFixture fixture)
        {
            this._serviceProvider = fixture.ServiceProvider;
            this._cityServiceMock = new Mock<ICityService>();
            this._externalServiceMock = new Mock<IExternalCityService>();
        }

        [Fact]
        public void Get_WhenCalled_ReturnsOkResultAndCityDTOList()
        {
            // Arrange
            this._cityServiceMock.Setup(svc => svc.RetrieveAllCities()).Returns(new List<City>
            {
                _cityMock
            });
            CitiesController controller = this.GenerateCitiesController();

            // Act
            var okResult = controller.Get();
            var returnedList = ((OkObjectResult)okResult.Result).Value as List<CityDTO>;

            // Assert
            Assert.IsType<OkObjectResult>(okResult.Result);
            Assert.True(returnedList.Count == 1);
            Assert.Collection(returnedList, cityDto =>
                Assert.True(cityDto.Name == "Salvador")
            );
        }

        [Fact]
        public void Post_WhenCalledRight_ReturnsStatusCreatedAndInsertedCityDTO()
        {
            // Arrange
            var mockedCityToDTO = _serviceProvider.GetService<IMapper>().Map<CityDTO>(_cityMock);

            this._cityServiceMock.Setup(svc => svc.CreateCity(It.Is<City>(
                (cty) => cty.Id == 0
                         && cty.Name == "Salvador"
                         && cty.CustomCode == "43534"
            ))).Returns(_cityMock);
            CitiesController controller = this.GenerateCitiesController();

            // Act
            var objectResult = controller.Post(mockedCityToDTO).Result as ObjectResult;
            var returnedCity = objectResult.Value as CityDTO;

            // Assert
            Assert.Equal(objectResult.StatusCode, (int)HttpStatusCode.Created);
            Assert.Equal("Salvador", returnedCity.Name);
            Assert.Equal("43534", returnedCity.CustomCode);
            Assert.Equal("BR", returnedCity.Country);
        }

        [Fact]
        public void GetForecast_WhenCalled_ReturnsOkAndCityForecastDTO()
        {
            // Arrange
            this._externalServiceMock.Setup(svc => svc.GetCityForecast(It.IsAny<string>()))
                .Returns(new CityForecastDTO());
            CitiesController controller = this.GenerateCitiesController();

            // Act
            var okResult = controller.GetForecast("x12345");

            // Assert
            Assert.IsType<OkObjectResult>(okResult.Result);
            Assert.IsType<CityForecastDTO>((okResult.Result as OkObjectResult).Value);
        }

        [Fact]
        public void SearchCities_WhenCalledWithLessThan3CharactersString_ReturnsBadRequest()
        {
            // Arrange 
            var controller = this.GenerateCitiesController();

            // Act
            var badRequestResult = controller.SearchCities("ab");

            // Assert
            Assert.Equal(400, (badRequestResult.Result as ObjectResult).StatusCode);
        }

        [Theory]
        [InlineData("Salvador")]
        [InlineData("São Paulo")]
        [InlineData("Rio de Janeiro")]
        public void SearchCities_WhenCalledRight_ReturnsListCityDTO(string cityName)
        {
            // Arrange 
            var listFoundCityDto = new List<FoundCityDTO> {
                new FoundCityDTO
                {
                    name = cityName,
                    id = 363563546
                },
                new FoundCityDTO
                {
                    name = "Outro nome estranho não deve ser retornado",
                    id = 123456
                }
            };
            this._externalServiceMock.Setup(svc => svc.SearchCitiesByName(It.Is<string>(q =>
                q == cityName))).Returns(new FoundCitiesDTO
                {
                    list = listFoundCityDto.Where(cty => cty.name == cityName).ToList()
                });
            var controller = this.GenerateCitiesController();

            // Act
            var okResult = controller.SearchCities(cityName);
            var foundCitiesDTO = (okResult.Result as OkObjectResult).Value as List<CityDTO>;

            // Assert
            Assert.IsType<OkObjectResult>(okResult.Result);
            Assert.True(foundCitiesDTO.Count == 1);
            Assert.All(foundCitiesDTO, foundCityDto =>
                Assert.Equal(cityName, foundCityDto.Name)
            );
        }



        private CitiesController GenerateCitiesController(Mock<ICityService> mockCityService = null,
            Mock<IExternalCityService> mockExternalService = null)
        {
            return new CitiesController(mockCityService?.Object ?? _cityServiceMock.Object,
                   _serviceProvider.GetService<IMapper>(),
                   mockExternalService?.Object ?? this._externalServiceMock.Object);
        }
    }
}
