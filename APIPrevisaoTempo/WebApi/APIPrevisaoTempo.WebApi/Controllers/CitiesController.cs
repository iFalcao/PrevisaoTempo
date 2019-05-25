using APIPrevisaoTempo.WebApi.DTOs;
using APIPrevisaoTempo.WebApi.Models;
using APIPrevisaoTempo.WebApi.Services;
using APIPrevisaoTempo.External.OpenWeatherProxy.Services;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using APIPrevisaoTempo.Common.Objects;

namespace APIPrevisaoTempo.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CitiesController : ControllerBase
    {
        private readonly ICityService _cityService;
        private readonly IMapper _mapper;
        private readonly ExternalCityService _externalCityService;

        public CitiesController(ICityService cityService, IMapper mapper, ExternalCityService externalCityService)
        {
            this._cityService = cityService;
            this._mapper = mapper;
            this._externalCityService = externalCityService;
        }

        // GET api/cities
        [HttpGet]
        public ActionResult<IEnumerable<CityDTO>> Get()
        {
            return Ok(this._cityService.RecoverAllCities());
        }

        // POST api/cities
        [HttpPost]
        public ActionResult Post(CityDTO newCity)
        {
            var insertedCity = this._cityService.CreateCity(_mapper.Map<City>(newCity));
            return StatusCode(201, _mapper.Map<CityDTO>(insertedCity));
        }

        [HttpGet]
        [Route("forecast/{customCode}")]
        public ActionResult GetForecast(string customCode)
        {
            CityForecastDTO cityForecastDTO = this._externalCityService.GetCityForecast(customCode);
            return Ok(cityForecastDTO);
        }

        [HttpGet]
        [Route("search/{cityName}")]
        public ActionResult SearchCities(string cityName)
        {
            FoundCitiesDTO foundCities = this._externalCityService.SearchCitiesByName(cityName);
            return Ok(foundCities);
        }

    }
}
