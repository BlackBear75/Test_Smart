using Microsoft.AspNetCore.Mvc;
using Moq;
using Test_Smart.Controllers;
using Test_Smart.Models.EquipmentTypeModels;
using Test_Smart.Service;

namespace Test_Smart.Test;

public class EquipmentTypeControllerTests
{
    private readonly Mock<IEquipmentTypeService> _mockService;
    private readonly EquipmentTypeController _controller;

    public EquipmentTypeControllerTests()
    {
        _mockService = new Mock<IEquipmentTypeService>();
        _controller = new EquipmentTypeController(_mockService.Object);
    }

    [Fact]
    public async Task GetAll_ReturnsOkResult()
    {
        // Arrange
        var equipmentTypes = new List<EquipmentTypeResponse>
        {
            new() { Id = Guid.NewGuid(), Code = "EQ001", Name = "Machine A", AreaPerUnit = 50 },
            new() { Id = Guid.NewGuid(), Code = "EQ002", Name = "Machine B", AreaPerUnit = 70 }
        };
        _mockService.Setup(service => service.GetAllAsync()).ReturnsAsync(equipmentTypes);

        // Act
        var result = await _controller.GetAll();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnValue = Assert.IsType<List<EquipmentTypeResponse>>(okResult.Value);
        Assert.Equal(2, returnValue.Count);
    }

    [Fact]
    public async Task GetById_ValidId_ReturnsOkResult()
    {
        // Arrange
        var equipmentTypeId = Guid.NewGuid();
        var equipmentType = new EquipmentTypeResponse
        {
            Id = equipmentTypeId, Code = "EQ001", Name = "Machine A", AreaPerUnit = 50
        };

        _mockService.Setup(service => service.GetByIdAsync(equipmentTypeId)).ReturnsAsync(equipmentType);

        // Act
        var result = await _controller.GetById(equipmentTypeId);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnValue = Assert.IsType<EquipmentTypeResponse>(okResult.Value);
        Assert.Equal("EQ001", returnValue.Code);
    }

    [Fact]
    public async Task GetById_InvalidId_ReturnsNotFound()
    {
        // Arrange
        var invalidId = Guid.NewGuid();
        _mockService.Setup(service => service.GetByIdAsync(invalidId))
            .ThrowsAsync(new KeyNotFoundException("EquipmentType not found."));

        // Act
        var result = await _controller.GetById(invalidId);

        // Assert
        var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
        var errorProperty = notFoundResult.Value.GetType().GetProperty("Error")?.GetValue(notFoundResult.Value, null);
        Assert.Equal("EquipmentType not found.", errorProperty);
    }

    [Fact]
    public async Task Create_ValidRequest_ReturnsOkResult()
    {
        // Arrange
        var request = new CreateEquipmentTypeRequest
        {
            Code = "EQ003", Name = "Machine C", AreaPerUnit = 100
        };

        // Act
        var result = await _controller.Create(request);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var messageProperty = okResult.Value.GetType().GetProperty("Message")?.GetValue(okResult.Value, null);
        Assert.Equal("EquipmentType created successfully.", messageProperty);

        _mockService.Verify(service => service.CreateAsync(request), Times.Once);
    }

    [Fact]
    public async Task Update_ValidRequest_ReturnsOkResult()
    {
        // Arrange
        var equipmentTypeId = Guid.NewGuid();
        var request = new UpdateEquipmentTypeRequest
        {
            Code = "EQ004", Name = "Machine D", AreaPerUnit = 200
        };

        // Act
        var result = await _controller.Update(equipmentTypeId, request);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var messageProperty = okResult.Value.GetType().GetProperty("Message")?.GetValue(okResult.Value, null);
        Assert.Equal("EquipmentType updated successfully.", messageProperty);

        _mockService.Verify(service => service.UpdateAsync(equipmentTypeId, request), Times.Once);
    }

    [Fact]
    public async Task Update_InvalidId_ReturnsNotFound()
    {
        // Arrange
        var invalidId = Guid.NewGuid();
        var request = new UpdateEquipmentTypeRequest
        {
            Code = "EQ004", Name = "Machine D", AreaPerUnit = 200
        };

        _mockService.Setup(service => service.UpdateAsync(invalidId, request))
            .ThrowsAsync(new KeyNotFoundException("EquipmentType not found."));

        // Act
        var result = await _controller.Update(invalidId, request);

        // Assert
        var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
        var errorProperty = notFoundResult.Value.GetType().GetProperty("Error")?.GetValue(notFoundResult.Value, null);
        Assert.Equal("EquipmentType not found.", errorProperty);
    }

    [Fact]
    public async Task Delete_ValidId_ReturnsOkResult()
    {
        // Arrange
        var equipmentTypeId = Guid.NewGuid();

        // Act
        var result = await _controller.Delete(equipmentTypeId);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var messageProperty = okResult.Value.GetType().GetProperty("Message")?.GetValue(okResult.Value, null);
        Assert.Equal("EquipmentType deleted successfully.", messageProperty);

        _mockService.Verify(service => service.DeleteAsync(equipmentTypeId), Times.Once);
    }

    [Fact]
    public async Task Delete_InvalidId_ReturnsNotFound()
    {
        // Arrange
        var invalidId = Guid.NewGuid();
        _mockService.Setup(service => service.DeleteAsync(invalidId))
            .ThrowsAsync(new KeyNotFoundException("EquipmentType not found."));

        // Act
        var result = await _controller.Delete(invalidId);

        // Assert
        var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
        var errorProperty = notFoundResult.Value.GetType().GetProperty("Error")?.GetValue(notFoundResult.Value, null);
        Assert.Equal("EquipmentType not found.", errorProperty);
    }
}