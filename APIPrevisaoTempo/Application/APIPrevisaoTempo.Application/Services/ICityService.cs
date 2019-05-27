using APIPrevisaoTempo.Domain.Models;
using System.Collections.Generic;

namespace APIPrevisaoTempo.Application.Services
{
    public interface ICityService
    {
        City CreateCity(City city);
        List<City> RetrieveAllCities();
    }
}
