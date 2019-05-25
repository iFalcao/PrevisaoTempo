using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIPrevisaoTempo.WebApi.DTOs
{
    public class CityDTO
    {
        public string Name { get; set; }

        public string CustomCode { get; set; }

        public double? Latitude { get; set; }

        public double? Longitude { get; set; }

        public string Country { get; set; }
    }
}
