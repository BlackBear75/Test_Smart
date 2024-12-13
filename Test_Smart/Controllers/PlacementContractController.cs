using Microsoft.AspNetCore.Mvc;
using Test_Smart.Models.PlacementContractModels;
using Test_Smart.Service;

namespace Test_Smart.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PlacementContractController : ControllerBase
{
    private readonly IPlacementContractService _placementContractService;

    public PlacementContractController(IPlacementContractService placementContractService)
    {
        _placementContractService = placementContractService;
    }

    [HttpPost("create")]
    public async Task<IActionResult> CreateContract([FromBody] CreateContractRequest request)
    {
        try
        {
            await _placementContractService.CreateContractAsync(request);
            return Ok(new { Message = "Contract created successfully." });
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { Error = ex.Message });
        }
    }

    [HttpGet("list")]
    public async Task<IActionResult> GetContracts()
    {
        var contracts = await _placementContractService.GetContractsAsync();
        return Ok(contracts);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetContractById(Guid id)
    {
        try
        {
            var contract = await _placementContractService.GetContractByIdAsync(id);
            return Ok(contract);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { Error = ex.Message });
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateContract(Guid id, [FromBody] UpdateContractRequest request)
    {
        try
        {
            await _placementContractService.UpdateContractAsync(id, request);
            return Ok(new { Message = "Contract updated successfully." });
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { Error = ex.Message });
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { Error = ex.Message });
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteContract(Guid id)
    {
        try
        {
            await _placementContractService.DeleteContractAsync(id);
            return Ok(new { Message = "Contract deleted successfully." });
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { Error = ex.Message });
        }
    }
}

