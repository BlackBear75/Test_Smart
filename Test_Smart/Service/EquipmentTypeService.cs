using Test_Smart.BackgroundServices;
using Test_Smart.Entity.EquipmentType;
using Test_Smart.Entity.EquipmentType.Repository;
using Test_Smart.Entity.PlacementContract;
using Test_Smart.Entity.PlacementContract.Repository;
using Test_Smart.Models.EquipmentTypeModels;

namespace Test_Smart.Service;

public interface IEquipmentTypeService
{
    Task<IEnumerable<EquipmentTypeResponse>> GetAllAsync();
    Task<EquipmentTypeResponse> GetByIdAsync(Guid id);
    Task CreateAsync(CreateEquipmentTypeRequest request);
    Task UpdateAsync(Guid id, UpdateEquipmentTypeRequest request);
    Task DeleteAsync(Guid id);
}

public class EquipmentTypeService : IEquipmentTypeService
{
    private readonly IEquipmentTypeRepository<EquipmentType> _equipmentTypeRepository;
    private readonly IPlacementContractRepository<PlacementContract> _placementRepository;
    private readonly LoggerBackgroundService _logger;

    public EquipmentTypeService(IEquipmentTypeRepository<EquipmentType> equipmentTypeRepository, LoggerBackgroundService logger, IPlacementContractRepository<PlacementContract> placementRepository)
    {
        _equipmentTypeRepository = equipmentTypeRepository;
        _placementRepository = placementRepository;
        _logger = logger;
    }

    public async Task<IEnumerable<EquipmentTypeResponse>> GetAllAsync()
    {
        var equipmentTypes = await _equipmentTypeRepository.GetAllAsync();
        return equipmentTypes.Select(e => new EquipmentTypeResponse
        {
            Id = e.Id,
            Code = e.Code,
            Name = e.Name,
            AreaPerUnit = e.AreaPerUnit
        });
    }

    public async Task<EquipmentTypeResponse> GetByIdAsync(Guid id)
    {
        var equipmentType = await _equipmentTypeRepository.FindByIdAsync(id);
        if (equipmentType == null) throw new KeyNotFoundException("EquipmentType not found.");

        return new EquipmentTypeResponse
        {
            Id = equipmentType.Id,
            Code = equipmentType.Code,
            Name = equipmentType.Name,
            AreaPerUnit = equipmentType.AreaPerUnit
        };
    }

    public async Task CreateAsync(CreateEquipmentTypeRequest request)
    {
        var equipmentType = new EquipmentType
        {
            Code = request.Code,
            Name = request.Name,
            AreaPerUnit = request.AreaPerUnit
        };

        await _equipmentTypeRepository.InsertOneAsync(equipmentType);
        _logger.Log($"Info: EquipmentType created: {request.Code}");
    }

    public async Task UpdateAsync(Guid id, UpdateEquipmentTypeRequest request)
    {
        var equipmentType = await _equipmentTypeRepository.FindByIdAsync(id);
        if (equipmentType == null) throw new KeyNotFoundException("EquipmentType not found.");

        equipmentType.Code = request.Code;
        equipmentType.Name = request.Name;
        equipmentType.AreaPerUnit = request.AreaPerUnit;

        await _equipmentTypeRepository.UpdateOneAsync(equipmentType);
        _logger.Log($"Info: EquipmentType updated: {request.Code}");
    }

    public async Task DeleteAsync(Guid id)
    {
        var equipmentType = await _equipmentTypeRepository.FindByIdAsync(id);
        if (equipmentType == null) 
            throw new KeyNotFoundException("EquipmentType not found.");

        var contracts = await _placementRepository.FilterByAsync(c => c.EquipmentTypeId == id && !c.Deleted);
        if (contracts.Any())
        {
            throw new InvalidOperationException("Cannot delete EquipmentType as it is associated with existing contracts.");
        }

        await _equipmentTypeRepository.DeleteOneAsync(id);
        _logger.Log($"Info: EquipmentType deleted: {equipmentType.Code}");
    }


}