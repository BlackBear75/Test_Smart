using System.Linq.Expressions;
using Test_Smart.Base.Repository;
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

    public PlacementContractService(
        IPlacementContractRepository<PlacementContract> contractRepository,
        IProductionFacilityRepository<ProductionFacility> facilityRepository,
        IEquipmentTypeRepository<EquipmentType> equipmentRepository)
    {
        _contractRepository = contractRepository;
        _facilityRepository = facilityRepository;
        _equipmentRepository = equipmentRepository;
    }

    public async Task CreateContractAsync(CreateContractRequest request)
    {
        var facility = await _facilityRepository.FindByIdAsync(request.ProductionFacilityId);
        if (facility == null)
        {
            throw new InvalidOperationException("Production facility not found.");
        }

        var equipment = await _equipmentRepository.FindByIdAsync(request.EquipmentTypeId);
        if (equipment == null)
        {
            throw new InvalidOperationException("Equipment type not found.");
        }

        var requiredArea = equipment.AreaPerUnit * request.Quantity;

        var contractsInFacility = await _contractRepository.FilterByAsync(c => c.ProductionFacilityId == request.ProductionFacilityId);
        var usedArea = contractsInFacility.Sum(c => c.Quantity * c.EquipmentType.AreaPerUnit);

        if (usedArea + requiredArea > facility.StandardArea)
        {
            throw new InvalidOperationException("Not enough available area in the production facility.");
        }

        var contract = new PlacementContract
        {
            ProductionFacilityId = request.ProductionFacilityId,
            EquipmentTypeId = request.EquipmentTypeId,
            Quantity = request.Quantity
        };

        await _contractRepository.InsertOneAsync(contract);
    }

    public async Task<IEnumerable<ContractResponse>> GetContractsAsync()
    {
        var contracts = await _contractRepository.GetAllWithIncludesAsync(
            c => c.ProductionFacility,
            c => c.EquipmentType
        );

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

        return responses;
    }


}
