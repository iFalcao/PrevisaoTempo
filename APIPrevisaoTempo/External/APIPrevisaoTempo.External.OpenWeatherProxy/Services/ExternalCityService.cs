﻿using APIPrevisaoTempo.External.OpenWeatherProxy.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace APIPrevisaoTempo.External.OpenWeatherProxy.Services
{
    public class ExternalCityService : BaseService
    {
        public ExternalCityService(IOptions<OpenWeatherApiConfiguration> customConfiguration)
        {
            base.Configuration = customConfiguration.Value;
        }

        /// <summary>
        /// Retrieve weather information of the city with the customCode informed in the next 5 days
        /// </summary>
        /// <param name="cityCustomCode">City's code on OpenWeatherApi</param>
        /// <returns></returns>
        public object GetCityForecast(string cityCustomCode)
        {
            string requestUrl = base.Configuration.BaseUrl 
                + $"forecast?id={cityCustomCode}&apiKey={base.Configuration.ApiKey}";
            string jsonResult = this.PerformQuery(requestUrl);
            return jsonResult;
        }

        public object SearchCitiesByName(string cityName)
        {
            string requestUrl = base.Configuration.BaseUrl 
                + $"find?q={cityName}&apiKey={base.Configuration.ApiKey}";
            string jsonResult = this.PerformQuery(requestUrl);
            return jsonResult;
        }

        /// <summary>
        /// Makes the GET request to OpenWeatherApi and returns the JSON
        /// </summary>
        /// <param name="requestUrl">Endpoint to make the GET request</param>
        /// <returns></returns>
        private string PerformQuery(string requestUrl)
        {
            HttpResponseMessage response = Client.GetAsync(requestUrl).Result;
            response.EnsureSuccessStatusCode();
            return response.Content.ReadAsStringAsync().Result;
        }
    }
}
