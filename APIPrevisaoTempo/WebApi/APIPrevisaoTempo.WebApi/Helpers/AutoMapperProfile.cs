using APIPrevisaoTempo.Common.Objects;
using APIPrevisaoTempo.WebApi.DTOs;
using APIPrevisaoTempo.WebApi.Models;
using AutoMapper;
using System.Collections.Generic;

namespace APIPrevisaoTempo.WebApi.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<FoundCityDTO, CityDTO>()
                .ForMember(dest => dest.Name, opt =>
                {
                    opt.MapFrom(src => src.name);
                })
                .ForMember(dest => dest.CustomCode, opt =>
                {
                    opt.MapFrom(src => src.id);
                })
                .ForMember(dest => dest.Latitude, opt =>
                {
                    opt.MapFrom(src => src.coord.lat);
                })
                .ForMember(dest => dest.Longitude, opt =>
                {
                    opt.MapFrom(src => src.coord.lon);
                })
                .ForMember(dest => dest.Country, opt =>
                {
                    opt.MapFrom(src => src.sys.country);
                });

            CreateMap<CityDTO, City>()
                .ForMember(dest => dest.Id, opt => 
                {
                    opt.Ignore();
                });
        }
    }
}
