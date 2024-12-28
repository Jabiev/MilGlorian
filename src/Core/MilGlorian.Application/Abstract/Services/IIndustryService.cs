using Microsoft.AspNetCore.Mvc;
using MilGlorian.Application.DTOs.Industry;
using MilGlorian.Common.Shared;

namespace MilGlorian.Application.Abstract.Services;

public interface IIndustryService
{
    Task<APIResponse<GetIndustryDTO>> GetByIdAsync(Guid id);
    Task<APIResponse<Pagination<GetIndustryDTO>>> GetAllAsync(int pageNumber = 1, int take = 10, bool isPaginated = false);
    Task<APIResponse<Pagination<GetIndustryDTO>>> SearchAsync(string name, int pageNumber = 1, int take = 10, bool isPaginated = false);
    Task<APIResponse<GetIndustryDTO>> CreateAsync(AddIndustryDTO createIndustryDTO);
    Task<APIResponse<UpdateIndustryDTO>> Update(Guid id, UpdateIndustryDTO updateIndustryDTO);
    Task<APIResponse<EmptyResult>> Delete(Guid id);
    //Companies in regard Industry
}
