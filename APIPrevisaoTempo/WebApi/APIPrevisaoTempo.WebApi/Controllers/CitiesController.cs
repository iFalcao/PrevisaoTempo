using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using APIPrevisaoTempo.WebApi.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace APIPrevisaoTempo.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CitiesController : ControllerBase
    {
        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<CityDTO>> Get()
        {
            return Ok();
        }

        // POST api/values
        [HttpPost]
        public ActionResult Post(CityDTO newCity)
        {
            return StatusCode(201);
        }
    }
}
