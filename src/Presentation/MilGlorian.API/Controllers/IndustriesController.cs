using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MilGlorian.Application.Abstract.Services;
using MilGlorian.Application.DTOs.Industry;

namespace MilGlorian.API.Controllers;

[Route("api/[controller]")]
[ApiController]
//[Authorize]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class IndustriesController : ControllerBase
{
    private readonly IIndustryService _industryService;

    public IndustriesController(IIndustryService industryService)
    {
        _industryService = industryService;
    }

    // GET: api/Industries
    [HttpGet]
    public async Task<ActionResult> GetIndustries(int pageNumber, int take, bool isPaginated)
    {
        var response = await _industryService.GetAllAsync(pageNumber, take, isPaginated);
        return response.ToActionResult();
    }

    // GET: api/Industries/5
    [HttpGet("{id}")]
    public async Task<ActionResult> GetIndustry([FromRoute] Guid id)
    {
        var response = await _industryService.GetByIdAsync(id);
        return response.ToActionResult();
    }

    // PUT: api/Industries/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateIndustry(Guid id, UpdateIndustryDTO updateIndustryDTO)
    {
        var response = await _industryService.Update(id, updateIndustryDTO);
        return response.ToActionResult();
    }

    // POST: api/Industries
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult> PostIndustry(AddIndustryDTO addIndustryDTO)
    {
        var response = await _industryService.CreateAsync(addIndustryDTO);
        return response.ToActionResult();
    }

    // DELETE: api/Industries/5
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteIndustry(Guid id)
    {
        var response = await _industryService.Delete(id);
        return response.ToActionResult();
    }
}
