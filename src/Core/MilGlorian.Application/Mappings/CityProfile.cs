using AutoMapper;
using MilGlorian.Application.DTOs.City;
using MilGlorian.Domain.Entities;

namespace MilGlorian.Application.Mappings;

public class CityProfile : Profile
{
    protected CityProfile()
    {
        CreateMap<City,GetCityDTO>().ReverseMap();
        CreateMap<City,CityDTO>().ReverseMap();
    }
}
