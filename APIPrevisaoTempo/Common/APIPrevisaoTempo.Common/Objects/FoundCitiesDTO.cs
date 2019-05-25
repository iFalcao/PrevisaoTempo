using System;
using System.Collections.Generic;
using System.Text;

namespace APIPrevisaoTempo.Common.Objects
{
    public class FoundCitiesDTO
    {
        public string message { get; set; }
        public string cod { get; set; }
        public int count { get; set; }
        public List<FoundCityDTO> list { get; set; }
    }

    public class FoundCityDTO
    {
        public int id { get; set; }
        public string name { get; set; }
        public CoordinatesDTO coord { get; set; }
        public MainTemperatureDTO main { get; set; }
        public long dt { get; set; }
        public WindDTO wind { get; set; }
        public CountryInfoDTO sys { get; set; }
        public RainInfoDTO rain { get; set; }
        public SnowInfoDTO snow { get; set; }
        public CloudInfoDTO clouds { get; set; }
        public List<WeatherMinimalInfoDTO> weather { get; set; }

    }

    public class RainInfoDTO
    {
    }

    public class SnowInfoDTO
    {
    }

    public class CountryInfoDTO
    {
        public string country { get; set; }
    }
}
