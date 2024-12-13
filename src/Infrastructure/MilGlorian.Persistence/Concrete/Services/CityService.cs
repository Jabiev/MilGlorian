using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MilGlorian.Application.Abstract.Repositories.Cities;
using MilGlorian.Application.Abstract.Services;
using MilGlorian.Application.DTOs.City;
using MilGlorian.Common.Shared;
using MilGlorian.Domain.Entities;
using MilGlorian.Persistence.Exceptions;

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

    public async Task<GetCityDTO> CreateAsync(CityDTO createCityDTO)
    {
        if (createCityDTO is null)
            throw new ArgumentNullException("the entity mustn't be null");

        if (string.IsNullOrEmpty(createCityDTO?.Name))
            throw new NullorEmptyException("Name can't be null");

        var city = _mapper.Map<City>(createCityDTO);
        await _writeRepository.AddAsync(city);
        await _writeRepository.SaveChangesAsync();

        return _mapper.Map<GetCityDTO>(city);
    }

    public async Task Delete(Guid id)
    {
        var entity = await _readRepository.GetByIdAsync(id);
        if (entity is null)
            throw new NotFoundException("the entity can't find");

        //depending branches or vacancies
        entity.isDeleted = true;
        await _writeRepository.SaveChangesAsync();
    }

    public async Task<Pagination<GetCityDTO>> GetAll(int pageNumber = 1, int take = 10, bool isPaginated = false)
    {
        var response = new Pagination<GetCityDTO>();

        if (pageNumber < 1 || take < 1)
            throw new ArgumentException("Page number and page size must be greater than zero");

        var query = _readRepository.GetAll();

        var items = await query
                .Skip((pageNumber - 1) * take)
                .Take(take)
                .ToListAsync();

        var mappedItems = _mapper.Map<List<GetCityDTO>>(items).ToList();

        response.Items = mappedItems;
        response.PageIndex = pageNumber;
        response.TotalCount = items.Count();

        if (!isPaginated)
        {
            items = await query.ToListAsync();
            mappedItems = _mapper.Map<List<GetCityDTO>>(items);

            response.Items = mappedItems;
            response.PageIndex = pageNumber;
            response.TotalCount = items.Count();
        }

        response.PageSize = take;
        return response;
    }

    public async Task<GetCityDTO> GetByIdAsync(Guid id)
    {
        var entity = await _readRepository.GetByIdAsync(id);
        if (entity is null)
            throw new NotFoundException("the entity can't find");
        return _mapper.Map<GetCityDTO>(entity);
    }

    public async Task<Pagination<GetCityDTO>> Search(string name, int pageNumber = 1, int take = 10, bool isPaginated = false)
    {
        var response = new Pagination<GetCityDTO>();

        if (string.IsNullOrEmpty(name))
            throw new ArgumentNullException("Search term cannot be null or empty");

        if (pageNumber < 1 || take < 1)
            throw new ArgumentException("Page number and page size must be greater than zero.");

        var query = _readRepository.Where(city => city.Name.ToLower().Contains(name.ToLower()));

        var items = await query
                .Skip((pageNumber - 1) * take)
                .Take(take)
                .ToListAsync();

        var mappedItems = _mapper.Map<List<GetCityDTO>>(items).ToList();

        response.Items = mappedItems;
        response.PageIndex = pageNumber;
        response.TotalCount = items.Count();

        if (!isPaginated)
        {
            items = await query.ToListAsync();
            mappedItems = _mapper.Map<List<GetCityDTO>>(items);

            response.Items = mappedItems;
            response.PageIndex = pageNumber;
            response.TotalCount = items.Count();
        }

        response.PageSize = take;
        return response;
    }

    public async Task<GetCityDTO> Update(Guid id, CityDTO updateCityDTO)
    {
        var entity = await _readRepository.GetByIdAsync(id);
        if (entity is null)
            throw new NotFoundException("the entity can't find");
        entity.Name = updateCityDTO.Name;
        await _writeRepository.SaveChangesAsync();

        return _mapper.Map<GetCityDTO>(entity);
    }
}
