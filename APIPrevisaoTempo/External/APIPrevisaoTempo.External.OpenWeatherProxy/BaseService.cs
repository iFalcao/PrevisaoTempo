using APIPrevisaoTempo.External.OpenWeatherProxy.Configuration;
using System.Net;
using System.Net.Http;

namespace APIPrevisaoTempo.External.OpenWeatherProxy
{
    public abstract class BaseService
    {
        protected static readonly HttpClient Client = new HttpClient();
        protected virtual OpenWeatherApiConfiguration Configuration { get; set; }
    }
}
