using APIPrevisaoTempo.WebApi.DTOs;
using APIPrevisaoTempo.WebApi.Models;
using AutoMapper;

namespace APIPrevisaoTempo.WebApi.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<City, CityDTO>();
            CreateMap<CityDTO, City>();
        }
    }
}
