using APIPrevisaoTempo.WebApi.Helpers;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace APIPrevisaoTempo.UnitTests
{
    public class ConfigFixture
    {
        public ConfigFixture()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddSingleton(provider => new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AutoMapperProfile());
            }).CreateMapper());
            ServiceProvider = serviceCollection.BuildServiceProvider();
        }

        public ServiceProvider ServiceProvider { get; private set; }
    }
}
