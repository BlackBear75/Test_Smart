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
    Task<ContractResponse> GetContractByIdAsync(Guid id);
    Task UpdateContractAsync(Guid id, UpdateContractRequest request);
    Task DeleteContractAsync(Guid id);
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
        var facility = await _facilityRepository.FindOneAsync(f => f.Code == request.ProductionFacilityCode)
            ?? throw new InvalidOperationException("Production facility not found.");

        var equipment = await _equipmentRepository.FindOneAsync(e => e.Code == request.EquipmentTypeCode)
            ?? throw new InvalidOperationException("Equipment type not found.");

        var requiredArea = equipment.AreaPerUnit * request.Quantity;

        ValidateAreaAvailability(facility, requiredArea, null);

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
        var contracts = await _contractRepository.GetAllWithIncludesAsync(
            c => c.ProductionFacility,
            c => c.EquipmentType);

        return contracts.Select(c => new ContractResponse
        {
            ProductionFacilityName = c.ProductionFacility?.Name ?? "Unknown",
            EquipmentTypeName = c.EquipmentType?.Name ?? "Unknown",
            Quantity = c.Quantity
        });
    }

    public async Task<ContractResponse> GetContractByIdAsync(Guid id)
    {
        var contract = await _contractRepository.GetByIdWithIncludesAsync(
            id,
            c => c.ProductionFacility,
            c => c.EquipmentType)
            ?? throw new KeyNotFoundException("Contract not found.");

        return new ContractResponse
        {
            ProductionFacilityName = contract.ProductionFacility?.Name ?? "Unknown",
            EquipmentTypeName = contract.EquipmentType?.Name ?? "Unknown",
            Quantity = contract.Quantity
        };
    }

    public async Task UpdateContractAsync(Guid id, UpdateContractRequest request)
    {
        var contract = await _contractRepository.FindByIdAsync(id)
            ?? throw new KeyNotFoundException("Contract not found.");

        var facility = await _facilityRepository.FindOneAsync(f => f.Code == request.ProductionFacilityCode)
            ?? throw new InvalidOperationException("Production facility not found.");

        var equipment = await _equipmentRepository.FindOneAsync(e => e.Code == request.EquipmentTypeCode)
            ?? throw new InvalidOperationException("Equipment type not found.");

        var requiredArea = equipment.AreaPerUnit * request.Quantity;

        ValidateAreaAvailability(facility, requiredArea, contract);

        contract.ProductionFacilityId = facility.Id;
        contract.EquipmentTypeId = equipment.Id;
        contract.Quantity = request.Quantity;

        await _contractRepository.UpdateOneAsync(contract);
        _loggerBackgroundService.Log($"Info: Contract updated successfully. ID={id}");
    }

    public async Task DeleteContractAsync(Guid id)
    {
        var contract = await _contractRepository.FindByIdAsync(id)
            ?? throw new KeyNotFoundException("Contract not found.");

        await _contractRepository.DeleteOneAsync(id);
        _loggerBackgroundService.Log($"Info: Contract deleted successfully. ID={id}");
    }

    private void ValidateAreaAvailability(ProductionFacility facility, double requiredArea, PlacementContract? existingContract)
    {
        var contractsInFacility = _contractRepository.FilterByAsync(c => c.ProductionFacilityId == facility.Id).Result;

        var usedArea = contractsInFacility
            .Where(c => existingContract == null || c.Id != existingContract.Id)
            .Sum(c => c.Quantity * c.EquipmentType.AreaPerUnit);

        if (usedArea + requiredArea > facility.StandardArea)
            throw new InvalidOperationException("Not enough available area in the production facility.");
    }
}
