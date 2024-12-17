using Microsoft.AspNetCore.Mvc;
using Moq;
using Test_Smart.Controllers;
using Test_Smart.Models.PlacementContractModels;
using Test_Smart.Service;

namespace Test_Smart.Test;

public class PlacementContractControllerTests
{
    private readonly Mock<IPlacementContractService> _mockService;
    private readonly PlacementContractController _controller;

    public PlacementContractControllerTests()
    {
        _mockService = new Mock<IPlacementContractService>();
        _controller = new PlacementContractController(_mockService.Object);
    }

    [Fact]
    public async Task CreateContract_ValidRequest_ReturnsOkResult()
    {
        // Arrange
        var request = new CreateContractRequest
        {
            ProductionFacilityCode = "FAC001",
            EquipmentTypeCode = "EQ001",
            Quantity = 10
        };

        // Act
        var result = await _controller.CreateContract(request);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var message = okResult.Value.GetType().GetProperty("Message")?.GetValue(okResult.Value, null);
        Assert.Equal("Contract created successfully.", message);

        _mockService.Verify(service => service.CreateContractAsync(request), Times.Once);
    }

    [Fact]
    public async Task CreateContract_InvalidRequest_ReturnsBadRequest()
    {
        // Arrange
        var request = new CreateContractRequest
        {
            ProductionFacilityCode = "FAC001",
            EquipmentTypeCode = "EQ001",
            Quantity = 10
        };

        _mockService.Setup(service => service.CreateContractAsync(request))
            .ThrowsAsync(new InvalidOperationException("Not enough available area in the production facility."));

        // Act
        var result = await _controller.CreateContract(request);

        // Assert
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
        var error = badRequestResult.Value.GetType().GetProperty("Error")?.GetValue(badRequestResult.Value, null);
        Assert.Equal("Not enough available area in the production facility.", error);
    }

    [Fact]
    public async Task GetContracts_ReturnsOkResult()
    {
        // Arrange
        var contracts = new List<ContractResponse>
        {
            new() { ProductionFacilityName = "Factory A", EquipmentTypeName = "Machine A", Quantity = 10 },
            new() { ProductionFacilityName = "Factory B", EquipmentTypeName = "Machine B", Quantity = 5 }
        };

        _mockService.Setup(service => service.GetContractsAsync()).ReturnsAsync(contracts);

        // Act
        var result = await _controller.GetContracts();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnValue = Assert.IsType<List<ContractResponse>>(okResult.Value);
        Assert.Equal(2, returnValue.Count);
    }

    [Fact]
    public async Task GetContractById_ValidId_ReturnsOkResult()
    {
        // Arrange
        var contractId = Guid.NewGuid();
        var contract = new ContractResponse
        {
            ProductionFacilityName = "Factory A",
            EquipmentTypeName = "Machine A",
            Quantity = 10
        };

        _mockService.Setup(service => service.GetContractByIdAsync(contractId)).ReturnsAsync(contract);

        // Act
        var result = await _controller.GetContractById(contractId);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnValue = Assert.IsType<ContractResponse>(okResult.Value);
        Assert.Equal("Factory A", returnValue.ProductionFacilityName);
    }

    [Fact]
    public async Task GetContractById_InvalidId_ReturnsNotFound()
    {
        // Arrange
        var contractId = Guid.NewGuid();

        _mockService.Setup(service => service.GetContractByIdAsync(contractId))
            .ThrowsAsync(new KeyNotFoundException("Contract not found."));

        // Act
        var result = await _controller.GetContractById(contractId);

        // Assert
        var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
        var error = notFoundResult.Value.GetType().GetProperty("Error")?.GetValue(notFoundResult.Value, null);
        Assert.Equal("Contract not found.", error);
    }

    [Fact]
    public async Task UpdateContract_ValidRequest_ReturnsOkResult()
    {
        // Arrange
        var contractId = Guid.NewGuid();
        var request = new UpdateContractRequest
        {
            ProductionFacilityCode = "FAC001",
            EquipmentTypeCode = "EQ001",
            Quantity = 15
        };

        // Act
        var result = await _controller.UpdateContract(contractId, request);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var message = okResult.Value.GetType().GetProperty("Message")?.GetValue(okResult.Value, null);
        Assert.Equal("Contract updated successfully.", message);

        _mockService.Verify(service => service.UpdateContractAsync(contractId, request), Times.Once);
    }

    [Fact]
    public async Task UpdateContract_InvalidId_ReturnsNotFound()
    {
        // Arrange
        var contractId = Guid.NewGuid();
        var request = new UpdateContractRequest
        {
            ProductionFacilityCode = "FAC001",
            EquipmentTypeCode = "EQ001",
            Quantity = 15
        };

        _mockService.Setup(service => service.UpdateContractAsync(contractId, request))
            .ThrowsAsync(new KeyNotFoundException("Contract not found."));

        // Act
        var result = await _controller.UpdateContract(contractId, request);

        // Assert
        var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
        var error = notFoundResult.Value.GetType().GetProperty("Error")?.GetValue(notFoundResult.Value, null);
        Assert.Equal("Contract not found.", error);
    }

    [Fact]
    public async Task DeleteContract_ValidId_ReturnsOkResult()
    {
        // Arrange
        var contractId = Guid.NewGuid();

        // Act
        var result = await _controller.DeleteContract(contractId);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var message = okResult.Value.GetType().GetProperty("Message")?.GetValue(okResult.Value, null);
        Assert.Equal("Contract deleted successfully.", message);

        _mockService.Verify(service => service.DeleteContractAsync(contractId), Times.Once);
    }

    [Fact]
    public async Task DeleteContract_InvalidId_ReturnsNotFound()
    {
        // Arrange
        var contractId = Guid.NewGuid();

        _mockService.Setup(service => service.DeleteContractAsync(contractId))
            .ThrowsAsync(new KeyNotFoundException("Contract not found."));

        // Act
        var result = await _controller.DeleteContract(contractId);

        // Assert
        var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
        var error = notFoundResult.Value.GetType().GetProperty("Error")?.GetValue(notFoundResult.Value, null);
        Assert.Equal("Contract not found.", error);
    }
}