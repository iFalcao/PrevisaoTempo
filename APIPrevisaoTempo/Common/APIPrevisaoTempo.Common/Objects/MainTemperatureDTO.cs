namespace APIPrevisaoTempo.Common.Objects
{
    public class MainTemperatureDTO
    {
        public double temp { get; set; }
        public double pressure { get; set; }
        public double humidity { get; set; }
        public double temp_min { get; set; }
        public double temp_max { get; set; }
        public double sea_level { get; set; }
        public double temp_kf { get; set; }
    }
}
