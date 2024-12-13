using Test_Smart.BackgroundServices;
using Test_Smart.Entity.EquipmentType;
using Test_Smart.Entity.EquipmentType.Repository;
using Test_Smart.Entity.PlacementContract;
using Test_Smart.Entity.PlacementContract.Repository;
using Test_Smart.Entity.ProductionFacility;
using Test_Smart.Entity.ProductionFacility.Repository;
using Test_Smart.Models.PlacementContractModels;

namespace Test_Smart.Service;

public interface IPlacementContractService 
{
    Task CreateContractAsync(CreateContractRequest request);
    Task<IEnumerable<ContractResponse>> GetContractsAsync();
}

public class PlacementContractService : IPlacementContractService
{
    private readonly IPlacementContractRepository<PlacementContract> _contractRepository;
    private readonly IProductionFacilityRepository<ProductionFacility> _facilityRepository;
    private readonly IEquipmentTypeRepository<EquipmentType> _equipmentRepository;
    private readonly LoggerBackgroundService _loggerBackgroundService;

    public PlacementContractService(
        IPlacementContractRepository<PlacementContract> contractRepository,
        IProductionFacilityRepository<ProductionFacility> facilityRepository,
        IEquipmentTypeRepository<EquipmentType> equipmentRepository,
        LoggerBackgroundService loggerBackgroundService)
    {
        _contractRepository = contractRepository;
        _facilityRepository = facilityRepository;
        _equipmentRepository = equipmentRepository;
        _loggerBackgroundService = loggerBackgroundService;
    }

    public async Task CreateContractAsync(CreateContractRequest request)
    {
        _loggerBackgroundService.Log("Starting CreateContractAsync process.");

        var facility = await _facilityRepository.FindOneAsync(f => f.Code == request.ProductionFacilityCode);
        if (facility == null)
        {
            var error = "Production facility not found.";
            _loggerBackgroundService.Log($"Error: {error}");
            throw new InvalidOperationException(error);
        }

        var equipment = await _equipmentRepository.FindOneAsync(e => e.Code == request.EquipmentTypeCode);
        if (equipment == null)
        {
            var error = "Equipment type not found.";
            _loggerBackgroundService.Log($"Error: {error}");
            throw new InvalidOperationException(error);
        }

        var requiredArea = equipment.AreaPerUnit * request.Quantity;

        var contractsInFacility = await _contractRepository.FilterByAsync(c => c.ProductionFacilityId == facility.Id);
        var usedArea = contractsInFacility.Sum(c =>
        {
            if (c.EquipmentType == null)
            {
                _loggerBackgroundService.Log($"Warning: Contract {c.Id} has a null EquipmentType.");
                return 0;
            }
            return c.Quantity * c.EquipmentType.AreaPerUnit;
        });

        if (usedArea + requiredArea > facility.StandardArea)
        {
            var error = "Not enough available area in the production facility.";
            _loggerBackgroundService.Log($"Error: {error}");
            throw new InvalidOperationException(error);
        }

        var contract = new PlacementContract
        {
            ProductionFacilityId = facility.Id,
            EquipmentTypeId = equipment.Id,
            Quantity = request.Quantity
        };

        await _contractRepository.InsertOneAsync(contract);

        _loggerBackgroundService.Log($"Info: Contract created successfully. Facility={facility.Name}, Equipment={equipment.Name}, Quantity={request.Quantity}");
    }

    public async Task<IEnumerable<ContractResponse>> GetContractsAsync()
    {
        try
        {
            _loggerBackgroundService.Log("Info: Starting GetContractsAsync process.");

            var contracts = await _contractRepository.GetAllWithIncludesAsync(
                c => c.ProductionFacility,
                c => c.EquipmentType
            );

            if (!contracts.Any())
            {
                _loggerBackgroundService.Log("Info: No contracts found.");
            }

            var responses = new List<ContractResponse>();

            foreach (var contract in contracts)
            {
                var response = new ContractResponse
                {
                    ProductionFacilityName = contract.ProductionFacility?.Name ?? "Unknown",
                    EquipmentTypeName = contract.EquipmentType?.Name ?? "Unknown",
                    Quantity = contract.Quantity
                };

                responses.Add(response);
            }

            _loggerBackgroundService.Log("Info: GetContractsAsync completed successfully.");
            return responses;
        }
        catch (Exception ex)
        {
            _loggerBackgroundService.Log($"Error: Failed to fetch contracts. {ex.Message}");
            throw; 
        }
    }

}
