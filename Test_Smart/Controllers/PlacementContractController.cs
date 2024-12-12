using Microsoft.AspNetCore.Mvc;
using Test_Smart.Models.PlacementContractModels;
using Test_Smart.Service;


namespace Test_Smart.Controllers
{
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
    }
}