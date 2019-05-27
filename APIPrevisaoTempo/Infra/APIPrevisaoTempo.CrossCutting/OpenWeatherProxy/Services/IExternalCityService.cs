using APIPrevisaoTempo.Infra.CrossCutting.Objects;

namespace APIPrevisaoTempo.Infra.CrossCutting.OpenWeatherProxy.Services
{
    public interface IExternalCityService
    {
        CityForecastDTO GetCityForecast(string cityCustomCode);
        FoundCitiesDTO SearchCitiesByName(string cityName);
    }
}