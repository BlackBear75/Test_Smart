using Microsoft.AspNetCore.Mvc;
using Test_Smart.Models.EquipmentTypeModels;
using Test_Smart.Service;

namespace Test_Smart.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EquipmentTypeController : ControllerBase
{
    private readonly IEquipmentTypeService _service;

    public EquipmentTypeController(IEquipmentTypeService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var equipmentTypes = await _service.GetAllAsync();
        return Ok(equipmentTypes);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        try
        {
            var equipmentType = await _service.GetByIdAsync(id);
            return Ok(equipmentType);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { Error = ex.Message });
        }
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateEquipmentTypeRequest request)
    {
        await _service.CreateAsync(request);
        return Ok(new { Message = "EquipmentType created successfully." });
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateEquipmentTypeRequest request)
    {
        try
        {
            await _service.UpdateAsync(id, request);
            return Ok(new { Message = "EquipmentType updated successfully." });
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
            await _service.DeleteAsync(id);
            return Ok(new { Message = "EquipmentType deleted successfully." });
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { Error = ex.Message });
        }
    }
}