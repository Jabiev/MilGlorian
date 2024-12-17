using AutoMapper;
using MilGlorian.Application.DTOs.City;
using MilGlorian.Domain.Entities;

namespace MilGlorian.Application.Mappings;

public class CityProfile : Profile
{
    public CityProfile()
    {
        CreateMap<City,GetCityDTO>().ReverseMap();
        CreateMap<City,AddCityDTO>().ReverseMap();
        CreateMap<City,UpdateCityDTO>().ReverseMap();
    }
}
