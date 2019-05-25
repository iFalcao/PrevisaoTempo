using APIPrevisaoTempo.WebApi.DTOs;
using APIPrevisaoTempo.WebApi.Models;
using APIPrevisaoTempo.WebApi.Services;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace APIPrevisaoTempo.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CitiesController : ControllerBase
    {
        public readonly ICityService _cityService;
        private readonly IMapper _mapper;

        public CitiesController(ICityService cityService, IMapper mapper)
        {
            this._cityService = cityService;
            _mapper = mapper;
        }

        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<CityDTO>> Get()
        {
            return Ok(this._cityService.RecoverAllCities());
        }

        // POST api/values
        [HttpPost]
        public ActionResult Post(CityDTO newCity)
        {
            var insertedCity = this._cityService.CreateCity(_mapper.Map<City>(newCity));
            return StatusCode(201, _mapper.Map<CityDTO>(insertedCity));
        }
    }
}
