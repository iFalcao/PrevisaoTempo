using APIPrevisaoTempo.Common.Objects;

namespace APIPrevisaoTempo.External.OpenWeatherProxy.Services
{
    public interface IExternalCityService
    {
        CityForecastDTO GetCityForecast(string cityCustomCode);
        FoundCitiesDTO SearchCitiesByName(string cityName);
    }
}