using APIPrevisaoTempo.Application.Services;
using APIPrevisaoTempo.Infra.CrossCutting.OpenWeatherProxy.Configuration;
using APIPrevisaoTempo.Infra.CrossCutting.OpenWeatherProxy.Services;
using APIPrevisaoTempo.Infra.Data.Context;
using APIPrevisaoTempo.Infra.Data.Repositoriy;
using APIPrevisaoTempo.Infra.Data.Repository;
using APIPrevisaoTempo.WebApi.Helpers;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using System;

namespace APIPrevisaoTempo.WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "API Previsão do Tempo", Version = "v1" });
            });

            services.AddDbContext<DataContext>(x =>
                x.UseSqlite(Configuration.GetConnectionString("DefaultConnection")
            ));

            services.AddCors();
            services.Configure<OpenWeatherApiConfiguration>(Configuration.GetSection("OpenWeatherApiConfiguration"));
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddScoped<ICityService, CityService>();
            services.AddScoped<ICityRepository, CityRepository>();
            services.AddScoped<IExternalCityService, ExternalCityService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                // Enable middleware to serve generated Swagger as a JSON endpoint.
                app.UseSwagger();

                // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), 
                // specifying the Swagger JSON endpoint.
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "API Previsão do Tempo - v1");
                });
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            
            app.ConfigureExceptionHandler();
            app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
