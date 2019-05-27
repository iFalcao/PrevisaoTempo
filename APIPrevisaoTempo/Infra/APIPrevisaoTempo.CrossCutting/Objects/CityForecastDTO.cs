using System.Collections.Generic;

namespace APIPrevisaoTempo.Infra.CrossCutting.Objects
{
    public class CityForecastDTO
    {
        public string message { get; set; }
        public string cod { get; set; }
        public int cnt { get; set; }
        public CityInfoDTO city { get; set; }
        public List<ForecastDTO> list { get; set; }
    }

    public class ForecastDTO
    {
        public long dt { get; set; }
        public MainTemperatureDTO main { get; set; }
        public List<WeatherMinimalInfoDTO> weather { get; set; }
        public CloudInfoDTO clouds { get; set; }
        public WindDTO wind { get; set; }
        public string dt_txt { get; set; }
    }

    public class CityInfoDTO
    {
        public long id { get; set; }
        public string name { get; set; }
        public string country { get; set; }
        public CoordinatesDTO coord { get; set; }
    }
}
