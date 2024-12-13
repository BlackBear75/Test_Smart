using Microsoft.AspNetCore.Mvc;
using Test_Smart.Models.ProductionFacilityModels;
using Test_Smart.Service;

namespace Test_Smart.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductionFacilityController : ControllerBase
{
    private readonly IProductionFacilityService _productionFacilityService;

    public ProductionFacilityController(IProductionFacilityService productionFacilityService)
    {
        _productionFacilityService = productionFacilityService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var facilities = await _productionFacilityService.GetAllAsync();
        return Ok(facilities);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        try
        {
            var facility = await _productionFacilityService.GetByIdAsync(id);
            return Ok(facility);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { Error = ex.Message });
        }
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateProductionFacilityRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        await _productionFacilityService.CreateProductionFacilityAsync(request);
        return Ok(new { Message = "Production Facility created successfully." });
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateProductionFacilityRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            await _productionFacilityService.UpdateAsync(id, request);
            return Ok(new { Message = "Production Facility updated successfully." });
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { Error = ex.Message });
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        try
        {
            await _productionFacilityService.DeleteAsync(id);
            return Ok(new { Message = "Production Facility deleted successfully." });
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { Error = ex.Message });
        }
    }
}
