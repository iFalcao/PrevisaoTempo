using APIPrevisaoTempo.Infra.CrossCutting.OpenWeatherProxy.Configuration;
using System.Net.Http;

namespace APIPrevisaoTempo.Infra.CrossCutting.OpenWeatherProxy
{
    public abstract class BaseService
    {
        protected static readonly HttpClient Client = new HttpClient();
        protected virtual OpenWeatherApiConfiguration Configuration { get; set; }
    }
}
