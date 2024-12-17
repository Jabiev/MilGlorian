using Microsoft.AspNetCore.Mvc;
using MilGlorian.Application.Abstract.Services;
using MilGlorian.Application.DTOs.City;
using MilGlorian.Common.Shared;
using MilGlorian.Domain.Entities;

namespace MilGlorian.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CitiesController : ControllerBase
{
    private readonly ICityService _cityService;

    public CitiesController(ICityService cityService)
    {
        _cityService = cityService;
    }

    // GET: api/Cities
    [HttpGet]
    public async Task<ActionResult<IEnumerable<GetCityDTO>>> GetCities(int pageNumber, int take, bool isPaginated)
    {
        var cities = await _cityService.GetAllAsync(pageNumber, take, isPaginated);
        return Ok(cities);
    }

    // GET: api/Cities/5
    [HttpGet("{id}")]
    public async Task<ActionResult<GetCityDTO>> GetCity([FromRoute] Guid id)
    {
        var city = await _cityService.GetByIdAsync(id);
        return Ok(city);
    }

    // PUT: api/Cities/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<ActionResult<UpdateCityDTO>> UpdateCity(Guid id, UpdateCityDTO updateCityDTO)
    {
        var updatedCity = await _cityService.Update(id, updateCityDTO);
        return Ok(updatedCity);
    }

    // POST: api/Cities
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<GetCityDTO>> PostCity(AddCityDTO addCityDTO)
    {
        var addedCity = await _cityService.CreateAsync(addCityDTO);
        return Ok(addedCity);
    }

    // DELETE: api/Cities/5
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteCity(Guid id)
    {
        await _cityService.Delete(id);
        return Ok();
    }
}
