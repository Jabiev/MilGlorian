using Microsoft.AspNetCore.Mvc;
using MilGlorian.Application.DTOs.City;
using MilGlorian.Common.Shared;

namespace MilGlorian.Application.Abstract.Services;

public interface ICityService
{
    Task<APIResponse<GetCityDTO>> GetByIdAsync(Guid id);
    Task<APIResponse<Pagination<GetCityDTO>>> GetAllAsync(int pageNumber = 1, int take = 10, bool isPaginated = false);
    Task<APIResponse<Pagination<GetCityDTO>>> SearchAsync(string name, int pageNumber = 1, int take = 10, bool isPaginated = false);
    Task<APIResponse<GetCityDTO>> CreateAsync(AddCityDTO createCityDTO);
    Task<APIResponse<UpdateCityDTO>> Update(Guid id, UpdateCityDTO updateCityDTO);
    Task<APIResponse<EmptyResult>> Delete(Guid id);
    //Vacancies in regard city
    //Branches in regard city
}
