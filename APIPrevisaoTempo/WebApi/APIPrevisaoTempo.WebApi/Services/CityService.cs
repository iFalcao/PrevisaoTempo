using APIPrevisaoTempo.WebApi.Models;
using APIPrevisaoTempo.WebApi.Repositories;
using System.Collections.Generic;

namespace APIPrevisaoTempo.WebApi.Services
{
    public class CityService : ICityService
    {
        public readonly ICityRepository _repository;

        public CityService(ICityRepository repository)
        {
            this._repository = repository;
        }

        public City CreateCity(City city)
        {
            return this._repository.Insert(city);
        }

        public List<City> RecoverAllCities()
        {
            return this._repository.SelectAll();
        }
    }
}
