namespace APIPrevisaoTempo.WebApi.Models
{
    public class City
    {
        public int Id { get; set; }

        public string Name { get; set; }

        /// <summary>
        /// 3rd party library, currently OpenWeather, uses this code as their City's ID
        /// </summary>
        public string CustomCode { get; set; }

        public double? Latitude { get; set; }

        public double? Longitude { get; set; }

        public string Country { get; set; }
    }
}
