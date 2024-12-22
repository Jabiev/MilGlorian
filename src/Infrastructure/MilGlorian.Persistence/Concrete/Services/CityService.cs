using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MilGlorian.Application.Abstract.Repositories.Cities;
using MilGlorian.Application.Abstract.Services;
using MilGlorian.Application.DTOs.City;
using MilGlorian.Common.Shared;
using MilGlorian.Domain.Entities;
using MilGlorian.Persistence.Exceptions;
using System.Net;

namespace MilGlorian.Persistence.Concrete.Services;

public class CityService : ICityService
{
    private readonly ICityReadRepository _readRepository;
    private readonly ICityWriteRepository _writeRepository;
    private readonly IMapper _mapper;

    public CityService(ICityReadRepository readRepository,
        ICityWriteRepository writeRepository,
        IMapper mapper)
    {
        _readRepository = readRepository;
        _writeRepository = writeRepository;
        _mapper = mapper;
    }

    public async Task<APIResponse<GetCityDTO>> CreateAsync(AddCityDTO createCityDTO)
    {
        var response = new APIResponse<GetCityDTO>();

        if (createCityDTO is null)
        {
            response.Message = "The entity mustn't be null";
            response.ResponseCode = HttpStatusCode.BadRequest;
            return response;
        }
        if ((await _readRepository.Where(c => c.Name == createCityDTO.Name).FirstOrDefaultAsync()) is not null)
        {
            response.Message = "Already Exists";
            response.ResponseCode = HttpStatusCode.BadRequest;
            return response;
        }

        var city = _mapper.Map<City>(createCityDTO);
        await _writeRepository.AddAsync(city);
        await _writeRepository.SaveChangesAsync();

        response.Payload = _mapper.Map<GetCityDTO>(city);
        response.ResponseCode = HttpStatusCode.OK;
        return response;
    }

    public async Task<APIResponse<EmptyResult>> Delete(Guid id)
    {
        var response = new APIResponse<EmptyResult>();

        var entity = await _readRepository.GetByIdAsync(id);
        if (entity is null || entity.isDeleted)
        {
            response.Message = "The entity can't find";
            response.ResponseCode = HttpStatusCode.NotFound;
            return response;
        }

        //depending branches or vacancies
        entity.isDeleted = true;
        await _writeRepository.SaveChangesAsync();
        response.ResponseCode = HttpStatusCode.OK;
        return response;
    }

    public async Task<APIResponse<Pagination<GetCityDTO>>> GetAllAsync(int pageNumber = 1, int take = 10, bool isPaginated = false)
    {
        var response = new APIResponse<Pagination<GetCityDTO>>();

        if (pageNumber < 1 || take < 1)
        {
            response.Message = "Page number and page size must be greater than zero";
            response.ResponseCode = HttpStatusCode.BadRequest;
            return response;
        }

        var query = _readRepository.GetAll(c => !c.isDeleted);

        var totalCount = await query.CountAsync();

        if (isPaginated)
            query = query
                    .Skip((pageNumber - 1) * take)
                    .Take(take);

        var mappedItems = _mapper.Map<List<GetCityDTO>>(query).ToList();

        response.Payload = new Pagination<GetCityDTO>()
        {
            Items = mappedItems,
            PageIndex = pageNumber,
            TotalCount = totalCount,
            TotalPage = (int)Math.Ceiling((double)totalCount / take),
            PageSize = isPaginated ? take : totalCount
        };
        response.ResponseCode = HttpStatusCode.OK;
        return response;
    }

    public async Task<APIResponse<GetCityDTO>> GetByIdAsync(Guid id)
    {
        var response = new APIResponse<GetCityDTO>();

        var entity = await _readRepository.GetByIdAsync(id);
        if (entity is null || entity.isDeleted)
        {
            response.Message = "The entity can't find";
            response.ResponseCode = HttpStatusCode.NotFound;
            return response;
        }

        response.Payload = _mapper.Map<GetCityDTO>(entity);
        response.ResponseCode = HttpStatusCode.OK;
        return response;
    }

    public async Task<APIResponse<Pagination<GetCityDTO>>> SearchAsync(string name, int pageNumber = 1, int take = 10, bool isPaginated = false)
    {
        var response = new APIResponse<Pagination<GetCityDTO>>();

        if (string.IsNullOrEmpty(name))
        {
            response.Message = "Search term cannot be null or empty";
            response.ResponseCode = HttpStatusCode.BadRequest;
            return response;
        }

        if (pageNumber < 1 || take < 1)
        {
            response.Message = "Page number and page size must be greater than zero.";
            response.ResponseCode = HttpStatusCode.BadRequest;
            return response;
        }

        var query = _readRepository.Where(city => city.Name.ToLower().Contains(name.ToLower()) && !city.isDeleted);

        var totalCount = await query.CountAsync();

        if (isPaginated)
            query = query
                    .Skip((pageNumber - 1) * take)
                    .Take(take);

        var mappedItems = _mapper.Map<List<GetCityDTO>>(query).ToList();

        response.Payload = new Pagination<GetCityDTO>()
        {
            Items = mappedItems,
            PageIndex = pageNumber,
            TotalCount = totalCount,
            TotalPage = (int)Math.Ceiling((double)totalCount / take),
            PageSize = isPaginated ? take : totalCount
        };
        response.ResponseCode = HttpStatusCode.OK;
        return response;
    }

    public async Task<APIResponse<UpdateCityDTO>> Update(Guid id, UpdateCityDTO updateCityDTO)
    {
        var response = new APIResponse<UpdateCityDTO>();

        if (id != updateCityDTO.Id)
        {
            response.Message = "Id must be similar the id which came from root";
            response.ResponseCode = HttpStatusCode.BadRequest;
            return response;
        }

        var entity = await _readRepository.GetByIdAsync(id);
        if (entity is null || entity.isDeleted)
        {
            response.Message = "The entity can't find";
            response.ResponseCode = HttpStatusCode.NotFound;
            return response;
        }

        entity.Name = updateCityDTO.Name;
        await _writeRepository.SaveChangesAsync();
        
        response.Payload = _mapper.Map<UpdateCityDTO>(entity);
        response.ResponseCode = HttpStatusCode.OK;

        return response;
    }
}
