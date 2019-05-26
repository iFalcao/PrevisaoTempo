using APIPrevisaoTempo.WebApi.Models;
using System.Collections.Generic;

namespace APIPrevisaoTempo.WebApi.Services
{
    public interface ICityService
    {
        City CreateCity(City city);
        List<City> RetrieveAllCities();
    }
}
