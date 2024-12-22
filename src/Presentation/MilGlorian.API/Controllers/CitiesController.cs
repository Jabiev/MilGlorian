using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MilGlorian.Application.Abstract.Services;
using MilGlorian.Application.DTOs.City;

namespace MilGlorian.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
//[Authorize(AuthenticationSchemes =JwtBearerDefaults.AuthenticationScheme)]
public class CitiesController : ControllerBase
{
    private readonly ICityService _cityService;

    public CitiesController(ICityService cityService)
    {
        _cityService = cityService;
    }

    // GET: api/Cities
    [HttpGet]
    public async Task<ActionResult> GetCities(int pageNumber, int take, bool isPaginated)
    {
        var response = await _cityService.GetAllAsync(pageNumber, take, isPaginated);
        return response.ToActionResult();
    }

    // GET: api/Cities/5
    [HttpGet("{id}")]
    public async Task<ActionResult> GetCity([FromRoute] Guid id)
    {
        var response = await _cityService.GetByIdAsync(id);
        return response.ToActionResult();
    }

    // PUT: api/Cities/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateCity(Guid id, UpdateCityDTO updateCityDTO)
    {
        var response = await _cityService.Update(id, updateCityDTO);
        return response.ToActionResult();
    }

    // POST: api/Cities
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult> PostCity(AddCityDTO addCityDTO)
    {
        var response = await _cityService.CreateAsync(addCityDTO);
        return response.ToActionResult();
    }

    // DELETE: api/Cities/5
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteCity(Guid id)
    {
        var response = await _cityService.Delete(id);
        return response.ToActionResult();
    }
}
