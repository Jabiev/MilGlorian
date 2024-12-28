using AutoMapper;
using MilGlorian.Application.DTOs.City;
using MilGlorian.Application.DTOs.Industry;
using MilGlorian.Domain.Entities;

namespace MilGlorian.Application.Mappings;

public class IndustryProfile : Profile
{
    public IndustryProfile()
    {
        CreateMap<Industry, GetIndustryDTO>().ReverseMap();
        CreateMap<Industry, AddIndustryDTO>().ReverseMap();
        CreateMap<Industry, UpdateIndustryDTO>().ReverseMap();
    }
}
