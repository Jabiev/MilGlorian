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
    [HttpGet("{pageNumber}/{take}/{isPaginated}")]
    public async Task<ActionResult<IEnumerable<GetCityDTO>>> GetCities([FromRoute] int pageNumber,
        [FromRoute] int take,
        [FromRoute] bool isPaginated)
    {
        var cities = new Pagination<GetCityDTO>();
        try
        {
            cities = await _cityService.GetAll(pageNumber, take, isPaginated);
        }
        catch (Exception ex)
        {
            return NotFound(ex.Message);
        }
        return Ok(cities);
    }

    // GET: api/Cities/5
    //[HttpGet("{id}")]
    //public async Task<ActionResult<City>> GetCity(Guid id)
    //{
    //    var city = await _context.Cities.FindAsync(id);

    //    if (city == null)
    //    {
    //        return NotFound();
    //    }

    //    return city;
    //}

    //// PUT: api/Cities/5
    //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    //[HttpPut("{id}")]
    //public async Task<IActionResult> PutCity(Guid id, City city)
    //{
    //    if (id != city.Id)
    //    {
    //        return BadRequest();
    //    }

    //    _context.Entry(city).State = EntityState.Modified;

    //    try
    //    {
    //        await _context.SaveChangesAsync();
    //    }
    //    catch (DbUpdateConcurrencyException)
    //    {
    //        if (!CityExists(id))
    //        {
    //            return NotFound();
    //        }
    //        else
    //        {
    //            throw;
    //        }
    //    }

    //    return NoContent();
    //}

    //// POST: api/Cities
    //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    //[HttpPost]
    //public async Task<ActionResult<City>> PostCity(City city)
    //{
    //    _context.Cities.Add(city);
    //    await _context.SaveChangesAsync();

    //    return CreatedAtAction("GetCity", new { id = city.Id }, city);
    //}

    //// DELETE: api/Cities/5
    //[HttpDelete("{id}")]
    //public async Task<IActionResult> DeleteCity(Guid id)
    //{
    //    var city = await _context.Cities.FindAsync(id);
    //    if (city == null)
    //    {
    //        return NotFound();
    //    }

    //    _context.Cities.Remove(city);
    //    await _context.SaveChangesAsync();

    //    return NoContent();
    //}

    //private bool CityExists(Guid id)
    //{
    //    return _context.Cities.Any(e => e.Id == id);
    //}
}
