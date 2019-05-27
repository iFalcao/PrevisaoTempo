using APIPrevisaoTempo.WebApi.DTOs;
using APIPrevisaoTempo.Domain.Models;
using APIPrevisaoTempo.Application.Services;
using APIPrevisaoTempo.Infra.CrossCutting.OpenWeatherProxy.Services;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using APIPrevisaoTempo.Infra.CrossCutting.Objects;
using System.Linq;
using System;

namespace APIPrevisaoTempo.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CitiesController : ControllerBase
    {
        private readonly ICityService _cityService;
        private readonly IMapper _mapper;
        private readonly IExternalCityService _externalCityService;

        public CitiesController(ICityService cityService, IMapper mapper, IExternalCityService externalCityService)
        {
            this._cityService = cityService;
            this._mapper = mapper;
            this._externalCityService = externalCityService;
        }

        // GET api/cities
        [HttpGet]
        public ActionResult<IEnumerable<CityDTO>> Get()
        {
            var retrievedCities = _mapper.Map<IEnumerable<CityDTO>>(this._cityService.RetrieveAllCities());
            return Ok(retrievedCities);
        }

        // POST api/cities
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public ActionResult<CityDTO> Post(CityDTO newCity)
        {
            var mappedCityDomain = _mapper.Map<City>(newCity);
            var insertedCity = this._cityService.CreateCity(mappedCityDomain);

            return CreatedAtAction("Post", _mapper.Map<CityDTO>(insertedCity));
        }

        [HttpGet]
        [Route("forecast/{customCode}")]
        public ActionResult<CityForecastDTO> GetForecast(string customCode)
        {
            CityForecastDTO cityForecastDTO = this._externalCityService.GetCityForecast(customCode);
            return Ok(cityForecastDTO);
        }

        [HttpGet]
        [Route("search/{cityName}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public ActionResult<List<CityDTO>> SearchCities(string cityName)
        {
            if (string.IsNullOrWhiteSpace(cityName) || cityName.Length < 3)
                return BadRequest(new ArgumentException("O nome da cidade deve ter no mínimo 3 caracteres."));

            FoundCitiesDTO foundCities = this._externalCityService.SearchCitiesByName(cityName);
            var convertedCities = foundCities.list
                .Select(foundCity => _mapper.Map<CityDTO>(foundCity))
                .ToList();
            return Ok(convertedCities);
        }

    }
}
