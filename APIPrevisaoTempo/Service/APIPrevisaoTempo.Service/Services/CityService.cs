using APIPrevisaoTempo.Infra.CrossCutting.OpenWeatherProxy.Services;
using APIPrevisaoTempo.Domain.Models;
using APIPrevisaoTempo.Infra.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace APIPrevisaoTempo.Application.Services
{
    public class CityService : ICityService
    {
        public readonly ICityRepository _repository;
        public readonly IExternalCityService _externalCityService;


        public CityService(ICityRepository repository, IExternalCityService externalCityService)
        {
            this._repository = repository;
            this._externalCityService = externalCityService;
        }

        /// <summary>
        /// Creates the city if valid
        /// </summary>
        /// <param name="city">City to create</param>
        /// <returns></returns>
        public City CreateCity(City city)
        {
            if (!this.CityExistsOnExternalApi(city))
                throw new ArgumentException("A cidade informada não está apta para recuperar informações sobre o clima.");
            if (!this.CityIsUnique(city))
                throw new ArgumentException("A cidade informada já foi cadastrada.");
            return this._repository.Insert(city);
        }

        /// <summary>
        /// Retrieve all cities on the DataSource
        /// </summary>
        /// <returns></returns>
        public List<City> RetrieveAllCities()
        {
            return this._repository.SelectAll();
        }


        /// <summary>
        /// Verify if the given city is available to use on external service
        /// </summary>
        /// <param name="city">City to verify availability</param>
        /// <returns></returns>
        private bool CityExistsOnExternalApi(City city)
        {
            var citiesWithSameName = _externalCityService.SearchCitiesByName(city.Name);
            return citiesWithSameName.count > 0 && citiesWithSameName.list.Any(cty => cty.id.ToString() == city.CustomCode);
        }

        /// <summary>
        /// The city is unique if there isn't any other cities with the same CustomCode
        /// </summary>
        /// <param name="city">City to check if is unique</param>
        /// <returns></returns>
        private bool CityIsUnique(City city)
        {
            return _repository.Where(cty => 
                cty.CustomCode.Equals(city.CustomCode)).Count() == 0;
        }
    }
}
