using APIPrevisaoTempo.WebApi.Models;
using System.Collections.Generic;

namespace APIPrevisaoTempo.WebApi.Repositories
{
    public interface ICityRepository
    {
        City Insert(City city);
        List<City> SelectAll();
    }
}
