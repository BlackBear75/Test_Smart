using Microsoft.AspNetCore.Mvc;
using Moq;
using Test_Smart.Controllers;
using Test_Smart.Models.ProductionFacilityModels;
using Test_Smart.Service;

namespace Test_Smart.Test;

public class ProductionFacilityControllerTests
{
    private readonly Mock<IProductionFacilityService> _mockService;
    private readonly ProductionFacilityController _controller;

    public ProductionFacilityControllerTests()
    {
        _mockService = new Mock<IProductionFacilityService>();
        _controller = new ProductionFacilityController(_mockService.Object);
    }

    [Fact]
    public async Task GetAll_ReturnsOkResult_WithListOfFacilities()
    {
        // Arrange
        var facilities = new List<ProductionFacilityResponse>
        {
            new ProductionFacilityResponse { Id = Guid.NewGuid(), Code = "FAC001", Name = "Factory A", StandardArea = 1000 },
            new ProductionFacilityResponse { Id = Guid.NewGuid(), Code = "FAC002", Name = "Factory B", StandardArea = 800 }
        };

        _mockService.Setup(service => service.GetAllAsync()).ReturnsAsync(facilities);

        // Act
        var result = await _controller.GetAll();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnedFacilities = Assert.IsType<List<ProductionFacilityResponse>>(okResult.Value);
        Assert.Equal(2, returnedFacilities.Count);
    }

    [Fact]
    public async Task GetById_ValidId_ReturnsOkResult_WithFacility()
    {
        // Arrange
        var facilityId = Guid.NewGuid();
        var facility = new ProductionFacilityResponse
        {
            Id = facilityId,
            Code = "FAC001",
            Name = "Factory A",
            StandardArea = 1000
        };

        _mockService.Setup(service => service.GetByIdAsync(facilityId)).ReturnsAsync(facility);

        // Act
        var result = await _controller.GetById(facilityId);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnedFacility = Assert.IsType<ProductionFacilityResponse>(okResult.Value);
        Assert.Equal(facilityId, returnedFacility.Id);
    }

    [Fact]
    public async Task GetById_InvalidId_ReturnsNotFound()
    {
        // Arrange
        var facilityId = Guid.NewGuid();

        _mockService.Setup(service => service.GetByIdAsync(facilityId))
            .ThrowsAsync(new KeyNotFoundException("ProductionFacility not found."));

        // Act
        var result = await _controller.GetById(facilityId);

        // Assert
        var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
        Assert.NotNull(notFoundResult.Value);

        var errorProperty = notFoundResult.Value.GetType().GetProperty("Error")?.GetValue(notFoundResult.Value, null);
        Assert.Equal("ProductionFacility not found.", errorProperty);
    }

    [Fact]
    public async Task Create_ValidRequest_ReturnsOkResult()
    {
        // Arrange
        var request = new CreateProductionFacilityRequest
        {
            Code = "FAC003",
            Name = "Factory C",
            StandardArea = 1200
        };

        // Act
        var result = await _controller.Create(request);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.NotNull(okResult.Value);

        // Перевірка поля "Message" у анонімному об'єкті
        var messageProperty = okResult.Value.GetType().GetProperty("Message")?.GetValue(okResult.Value, null);
        Assert.Equal("Production Facility created successfully.", messageProperty);

        _mockService.Verify(service => service.CreateProductionFacilityAsync(request), Times.Once);
    }


    [Fact]
    public async Task Create_InvalidRequest_ReturnsBadRequest()
    {
        // Arrange
        var request = new CreateProductionFacilityRequest(); // Missing required fields
        _controller.ModelState.AddModelError("Name", "The Name field is required.");

        // Act
        var result = await _controller.Create(request);

        // Assert
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
        Assert.IsType<SerializableError>(badRequestResult.Value);
    }

    [Fact]
    public async Task Update_ValidRequest_ReturnsOkResult()
    {
        // Arrange
        var facilityId = Guid.NewGuid();
        var request = new UpdateProductionFacilityRequest
        {
            Code = "FAC003",
            Name = "Factory C Updated",
            StandardArea = 1300
        };

        // Act
        var result = await _controller.Update(facilityId, request);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.NotNull(okResult.Value);

        // Отримуємо значення поля "Message" через рефлексію
        var messageProperty = okResult.Value.GetType().GetProperty("Message")?.GetValue(okResult.Value, null);
        Assert.Equal("Production Facility updated successfully.", messageProperty);

        // Перевіряємо виклик сервісу
        _mockService.Verify(service => service.UpdateAsync(facilityId, request), Times.Once);
    }

    [Fact]
    public async Task Update_InvalidId_ReturnsNotFound()
    {
        // Arrange
        var facilityId = Guid.NewGuid();
        var request = new UpdateProductionFacilityRequest
        {
            Code = "FAC003",
            Name = "Factory C Updated",
            StandardArea = 1300
        };

        _mockService.Setup(service => service.UpdateAsync(facilityId, request))
            .ThrowsAsync(new KeyNotFoundException("ProductionFacility not found."));

        // Act
        var result = await _controller.Update(facilityId, request);

        // Assert
        var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
        Assert.NotNull(notFoundResult.Value);

        // Перевірка поля "Error" у анонімному об'єкті
        var errorProperty = notFoundResult.Value.GetType().GetProperty("Error")?.GetValue(notFoundResult.Value, null);
        Assert.Equal("ProductionFacility not found.", errorProperty);
    }

    [Fact]
    public async Task Delete_ValidId_ReturnsOkResult()
    {
        // Arrange
        var facilityId = Guid.NewGuid();

        // Act
        var result = await _controller.Delete(facilityId);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.NotNull(okResult.Value);

        var messageProperty = okResult.Value.GetType().GetProperty("Message")?.GetValue(okResult.Value, null);
        Assert.Equal("Production Facility deleted successfully.", messageProperty);

        _mockService.Verify(service => service.DeleteAsync(facilityId), Times.Once);
    }

    [Fact]
    public async Task Delete_InvalidId_ReturnsNotFound()
    {
        // Arrange
        var facilityId = Guid.NewGuid();

        _mockService.Setup(service => service.DeleteAsync(facilityId))
            .ThrowsAsync(new KeyNotFoundException("ProductionFacility not found."));

        // Act
        var result = await _controller.Delete(facilityId);

        // Assert
        var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
        Assert.NotNull(notFoundResult.Value);

        var errorProperty = notFoundResult.Value.GetType().GetProperty("Error")?.GetValue(notFoundResult.Value, null);
        Assert.Equal("ProductionFacility not found.", errorProperty);
    }

}