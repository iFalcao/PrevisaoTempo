using APIPrevisaoTempo.External.OpenWeatherProxy.Services;
using APIPrevisaoTempo.WebApi.Controllers;
using APIPrevisaoTempo.WebApi.Services;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Xunit;
using Moq;
using System.Collections.Generic;
using APIPrevisaoTempo.WebApi.Models;
using System.Linq;
using APIPrevisaoTempo.WebApi.DTOs;
using System.Net;
using APIPrevisaoTempo.Common.Objects;

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
            this._cityServiceMock.Setup(svc => svc.RecoverAllCities()).Returns(new List<City>
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
        public void Post_WhenCalled_ReturnsStatusCreatedAndInsertedCityDTO()
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
            var objectResult = (ObjectResult)controller.Post(mockedCityToDTO);
            var returnedCity = objectResult.Value as CityDTO;

            // Assert
            Assert.Equal(objectResult.StatusCode.Value, (int)HttpStatusCode.Created);
            Assert.Equal("Salvador", returnedCity.Name);
            Assert.Equal("43534", returnedCity.CustomCode);
            Assert.Equal("BR", returnedCity.Country);
        }

        [Fact]
        public void SearchCities_WhenCalledWithLessThan3CharactersString_ReturnsBadRequest()
        {
            // Arrange 
            var controller = this.GenerateCitiesController();

            // Act
            var badRequestResult = controller.SearchCities("ab");

            // Assert
            Assert.IsType<BadRequestObjectResult>(badRequestResult);
            Assert.Equal(400, (int)(badRequestResult as BadRequestObjectResult).StatusCode);
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
            var foundCitiesDTO = (okResult as OkObjectResult).Value as List<CityDTO>;

            // Assert
            Assert.IsType<OkObjectResult>(okResult);
            Assert.True(foundCitiesDTO.Count == 1);
            Assert.All(foundCitiesDTO, foundCityDto =>
                Assert.Equal(cityName, foundCityDto.Name)
            );
        }



        private CitiesController GenerateCitiesController(Mock<ICityService> mockCityService = null, Mock<IExternalCityService> mockExternalService = null)
        {
            return new CitiesController(mockCityService?.Object ?? _cityServiceMock.Object,
                   _serviceProvider.GetService<IMapper>(),
                   mockExternalService?.Object ?? this._externalServiceMock.Object);
        }
    }
}
