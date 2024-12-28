using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MilGlorian.Application.Abstract.Repositories.Industries;
using MilGlorian.Application.Abstract.Services;
using MilGlorian.Application.DTOs.Industry;
using MilGlorian.Application.Validators.Industry;
using MilGlorian.Common.Shared;
using MilGlorian.Domain.Entities;
using System.Net;
using System.Text;

namespace MilGlorian.Persistence.Concrete.Services;

public class IndustryService : IIndustryService
{
    private readonly IIndustryReadRepository _readRepository;
    private readonly IIndustryWriteRepository _writeRepository;
    private readonly IMapper _mapper;

    public IndustryService(IIndustryReadRepository readRepository,
        IIndustryWriteRepository writeRepository,
        IMapper mapper)
    {
        _readRepository = readRepository;
        _writeRepository = writeRepository;
        _mapper = mapper;
    }

    public async Task<APIResponse<GetIndustryDTO>> CreateAsync(AddIndustryDTO createIndustryDTO)
    {
        var response = new APIResponse<GetIndustryDTO>();

        AddIndustryDTOValidator validations = new();

        var result = await validations.ValidateAsync(createIndustryDTO);

        if (!result.IsValid)
        {
            StringBuilder stringBuilder = new();
            foreach (var error in result.Errors)
                stringBuilder.AppendLine(error.ErrorMessage);
            response.ResponseCode = HttpStatusCode.BadRequest;
            response.Message = stringBuilder.ToString();
            return response;
        }

        if (createIndustryDTO is null)
        {
            response.Message = "The entity mustn't be null";
            response.ResponseCode = HttpStatusCode.BadRequest;
            return response;
        }
        if ((await _readRepository.Where(c => c.Name == createIndustryDTO.Name).FirstOrDefaultAsync()) is not null)
        {
            response.Message = "Already Exists";
            response.ResponseCode = HttpStatusCode.BadRequest;
            return response;
        }

        var industry = _mapper.Map<Industry>(createIndustryDTO);
        await _writeRepository.AddAsync(industry);
        await _writeRepository.SaveChangesAsync();

        response.Payload = _mapper.Map<GetIndustryDTO>(industry);
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

        //depending companies
        entity.isDeleted = true;
        await _writeRepository.SaveChangesAsync();
        response.ResponseCode = HttpStatusCode.OK;
        return response;
    }

    public async Task<APIResponse<Pagination<GetIndustryDTO>>> GetAllAsync(int pageNumber = 1, int take = 10, bool isPaginated = false)
    {
        var response = new APIResponse<Pagination<GetIndustryDTO>>();

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

        var mappedItems = _mapper.Map<List<GetIndustryDTO>>(query).ToList();

        response.Payload = new Pagination<GetIndustryDTO>()
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

    public async Task<APIResponse<GetIndustryDTO>> GetByIdAsync(Guid id)
    {
        var response = new APIResponse<GetIndustryDTO>();

        var entity = await _readRepository.GetByIdAsync(id);
        if (entity is null || entity.isDeleted)
        {
            response.Message = "The entity can't find";
            response.ResponseCode = HttpStatusCode.NotFound;
            return response;
        }

        response.Payload = _mapper.Map<GetIndustryDTO>(entity);
        response.ResponseCode = HttpStatusCode.OK;
        return response;
    }

    public async Task<APIResponse<Pagination<GetIndustryDTO>>> SearchAsync(string name, int pageNumber = 1, int take = 10, bool isPaginated = false)
    {
        var response = new APIResponse<Pagination<GetIndustryDTO>>();

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

        var mappedItems = _mapper.Map<List<GetIndustryDTO>>(query).ToList();

        response.Payload = new Pagination<GetIndustryDTO>()
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

    public async Task<APIResponse<UpdateIndustryDTO>> Update(Guid id, UpdateIndustryDTO updateIndustryDTO)
    {
        var response = new APIResponse<UpdateIndustryDTO>();

        UpdateIndustryDTOValidator validations = new();

        var result = await validations.ValidateAsync(updateIndustryDTO);

        if (!result.IsValid)
        {
            StringBuilder stringBuilder = new();
            foreach (var error in result.Errors)
                stringBuilder.AppendLine(error.ErrorMessage);
            response.ResponseCode = HttpStatusCode.BadRequest;
            response.Message = stringBuilder.ToString();
            return response;
        }

        if (id != updateIndustryDTO.Id)
        {
            response.Message = "Id must be similar the id which came from route";
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

        if ((await _readRepository.FirstOrDefaultAsync(c => c.Name == updateIndustryDTO.Name)) is not null)
        {
            response.Message = "The entity already exists";
            response.ResponseCode = HttpStatusCode.NotFound;
            return response;
        }

        entity.Name = updateIndustryDTO.Name;
        await _writeRepository.SaveChangesAsync();

        response.Payload = _mapper.Map<UpdateIndustryDTO>(entity);
        response.ResponseCode = HttpStatusCode.OK;

        return response;
    }
}
