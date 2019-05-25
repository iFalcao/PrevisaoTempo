using APIPrevisaoTempo.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIPrevisaoTempo.WebApi.Services
{
    public interface ICityService
    {
        City CreateCity(City city);
        List<City> RecoverAllCities();
    }
}
