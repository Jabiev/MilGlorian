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

    public async Task<GetCityDTO> CreateAsync(AddCityDTO createCityDTO)
    {
        if (createCityDTO is null)
            throw new ArgumentNullException("the entity mustn't be null");
        if ((await _readRepository.Where(c => c.Name == createCityDTO.Name).FirstOrDefaultAsync()) is not null)
            throw new Exception("Already Exists");

        var city = _mapper.Map<City>(createCityDTO);
        await _writeRepository.AddAsync(city);
        await _writeRepository.SaveChangesAsync();

        return _mapper.Map<GetCityDTO>(city);
    }

    public async Task Delete(Guid id)
    {
        var entity = await _readRepository.GetByIdAsync(id);
        if (entity is null || entity.isDeleted)
            throw new NotFoundException("the entity can't find");

        //depending branches or vacancies
        entity.isDeleted = true;
        await _writeRepository.SaveChangesAsync();
    }

    public async Task<Pagination<GetCityDTO>> GetAllAsync(int pageNumber = 1, int take = 10, bool isPaginated = false)
    {
        if (pageNumber < 1 || take < 1)
            throw new ArgumentException("Page number and page size must be greater than zero");

        var query = _readRepository.GetAll(c => !c.isDeleted);

        var totalCount = await query.CountAsync();

        if (isPaginated)
            query = query
                    .Skip((pageNumber - 1) * take)
                    .Take(take);

        var mappedItems = _mapper.Map<List<GetCityDTO>>(query).ToList();

        var response = new Pagination<GetCityDTO>()
        {
            Items = mappedItems,
            PageIndex = pageNumber,
            TotalCount = totalCount,
            TotalPage = (int)Math.Ceiling((double)totalCount / take),
            PageSize = isPaginated ? take : totalCount
        };
        return response;
    }

    public async Task<GetCityDTO> GetByIdAsync(Guid id)
    {
        var entity = await _readRepository.GetByIdAsync(id);
        if (entity is null || entity.isDeleted)
            throw new NotFoundException("The entity can't find");
        return _mapper.Map<GetCityDTO>(entity);
    }

    public async Task<Pagination<GetCityDTO>> SearchAsync(string name, int pageNumber = 1, int take = 10, bool isPaginated = false)
    {
        if (string.IsNullOrEmpty(name))
            throw new ArgumentNullException("Search term cannot be null or empty");

        if (pageNumber < 1 || take < 1)
            throw new ArgumentException("Page number and page size must be greater than zero.");

        var query = _readRepository.Where(city => city.Name.ToLower().Contains(name.ToLower()) && !city.isDeleted);

        var totalCount = await query.CountAsync();

        if (isPaginated)
            query = query
                    .Skip((pageNumber - 1) * take)
                    .Take(take);

        var mappedItems = _mapper.Map<List<GetCityDTO>>(query).ToList();

        var response = new Pagination<GetCityDTO>()
        {
            Items = mappedItems,
            PageIndex = pageNumber,
            TotalCount = totalCount,
            TotalPage = (int)Math.Ceiling((double)totalCount / take),
            PageSize = isPaginated ? take : totalCount
        };

        return response;
    }

    public async Task<UpdateCityDTO> Update(Guid id, UpdateCityDTO updateCityDTO)
    {
        if (id != updateCityDTO.Id)
            throw new Exception("Id must be similar the id which came from root");

        var entity = await _readRepository.GetByIdAsync(id);
        if (entity is null || entity.isDeleted)
            throw new NotFoundException("the entity can't find");

        entity.Name = updateCityDTO.Name;
        await _writeRepository.SaveChangesAsync();

        return _mapper.Map<UpdateCityDTO>(entity);
    }
}
