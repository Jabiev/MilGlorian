using MilGlorian.Application.DTOs.City;
using MilGlorian.Common.Shared;

namespace MilGlorian.Application.Abstract.Services;

public interface ICityService
{
    Task<GetCityDTO> GetByIdAsync(Guid id);
    Task<Pagination<GetCityDTO>> GetAllAsync(int pageNumber = 1, int take = 10, bool isPaginated = false);
    Task<Pagination<GetCityDTO>> SearchAsync(string name, int pageNumber = 1, int take = 10, bool isPaginated = false);
    Task<GetCityDTO> CreateAsync(AddCityDTO createCityDTO);
    Task<UpdateCityDTO> Update(Guid id, UpdateCityDTO updateCityDTO);
    Task Delete(Guid id);
    //Vacancies in regard city
    //Branches in regard city
}
