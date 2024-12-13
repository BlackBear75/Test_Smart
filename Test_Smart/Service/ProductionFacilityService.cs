using Test_Smart.BackgroundServices;
using Test_Smart.Entity.PlacementContract;
using Test_Smart.Entity.PlacementContract.Repository;
using Test_Smart.Entity.ProductionFacility;
using Test_Smart.Entity.ProductionFacility.Repository;
using Test_Smart.Models.ProductionFacilityModels;

namespace Test_Smart.Service;

public interface IProductionFacilityService
{
    Task<IEnumerable<ProductionFacilityResponse>> GetAllAsync();
    Task<ProductionFacilityResponse> GetByIdAsync(Guid id);
    Task CreateProductionFacilityAsync(CreateProductionFacilityRequest request);
    Task UpdateAsync(Guid id, UpdateProductionFacilityRequest request);
    Task DeleteAsync(Guid id);
}

public class ProductionFacilityService : IProductionFacilityService
{
    private readonly IProductionFacilityRepository<ProductionFacility> _repository;
    private readonly IPlacementContractRepository<PlacementContract> _placementRepository;
    private readonly LoggerBackgroundService _logger;

    public ProductionFacilityService(
        IProductionFacilityRepository<ProductionFacility> repository, 
        IPlacementContractRepository<PlacementContract> placementRepository,
        LoggerBackgroundService logger)
    {
        _repository = repository;
        _placementRepository = placementRepository;
        _logger = logger;
    }

    public async Task<IEnumerable<ProductionFacilityResponse>> GetAllAsync()
    {
        var facilities = await _repository.GetAllAsync();
        return facilities.Select(f => new ProductionFacilityResponse
        {
            Id = f.Id,
            Code = f.Code,
            Name = f.Name,
            StandardArea = f.StandardArea
        });
    }

    public async Task<ProductionFacilityResponse> GetByIdAsync(Guid id)
    {
        var facility = await _repository.FindByIdAsync(id);
        if (facility == null) throw new KeyNotFoundException("ProductionFacility not found.");

        return new ProductionFacilityResponse
        {
            Id = facility.Id,
            Code = facility.Code,
            Name = facility.Name,
            StandardArea = facility.StandardArea
        };
    }

    public async Task CreateProductionFacilityAsync(CreateProductionFacilityRequest request)
    {
        var facility = new ProductionFacility
        {
            Code = request.Code,
            Name = request.Name,
            StandardArea = request.StandardArea
        };

        await _repository.InsertOneAsync(facility);
        _logger.Log($"Info: ProductionFacility created: {request.Code}");
    }

    public async Task UpdateAsync(Guid id, UpdateProductionFacilityRequest request)
    {
        var facility = await _repository.FindByIdAsync(id);
        if (facility == null) throw new KeyNotFoundException("ProductionFacility not found.");

        facility.Code = request.Code;
        facility.Name = request.Name;
        facility.StandardArea = request.StandardArea;

        await _repository.UpdateOneAsync(facility);
        _logger.Log($"Info: ProductionFacility updated: {request.Code}");
    }

    public async Task DeleteAsync(Guid id)
    {
        var facility = await _repository.FindByIdAsync(id);
        if (facility == null) 
            throw new KeyNotFoundException("ProductionFacility not found.");

        var contracts = await _placementRepository.FilterByAsync(c => c.ProductionFacilityId == id && !c.Deleted);
        if (contracts.Any())
        {
            throw new InvalidOperationException("Cannot delete ProductionFacility as it is associated with existing contracts.");
        }

        await _repository.DeleteOneAsync(id);
        _logger.Log($"Info: ProductionFacility deleted: {facility.Code}");
    }
}
