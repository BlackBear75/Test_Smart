using Microsoft.EntityFrameworkCore;
using Test_Smart.Entity.EquipmentType;
using Test_Smart.Entity.PlacementContract;
using Test_Smart.Entity.ProductionFacility;

namespace Test_Smart.Configuration;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        Database.EnsureCreated();
    }

    public DbSet<ProductionFacility> ProductionFacilities { get; set; }
    public DbSet<EquipmentType> EquipmentTypes { get; set; }
    public DbSet<PlacementContract> PlacementContracts { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        var facility1 = new ProductionFacility
        {
            Id = Guid.NewGuid(),
            Code = "FAC001",
            Name = "Factory A",
            StandardArea = 1000
        };

        var facility2 = new ProductionFacility
        {
            Id = Guid.NewGuid(),
            Code = "FAC002",
            Name = "Factory B",
            StandardArea = 800
        };

        var facility3 = new ProductionFacility
        {
            Id = Guid.NewGuid(),
            Code = "FAC003",
            Name = "Factory C",
            StandardArea = 1200
        };

        var equipment1 = new EquipmentType
        {
            Id = Guid.NewGuid(),
            Code = "EQ001",
            Name = "Machine A",
            AreaPerUnit = 50
        };

        var equipment2 = new EquipmentType
        {
            Id = Guid.NewGuid(),
            Code = "EQ002",
            Name = "Machine B",
            AreaPerUnit = 70
        };

        var equipment3 = new EquipmentType
        {
            Id = Guid.NewGuid(),
            Code = "EQ003",
            Name = "Machine C",
            AreaPerUnit = 30
        };

        var equipment4 = new EquipmentType
        {
            Id = Guid.NewGuid(),
            Code = "EQ004",
            Name = "Machine D",
            AreaPerUnit = 90
        };

        var contract1 = new PlacementContract
        {
            Id = Guid.NewGuid(),
            ProductionFacilityId = facility1.Id,
            EquipmentTypeId = equipment1.Id,
            Quantity = 10
        };

        var contract2 = new PlacementContract
        {
            Id = Guid.NewGuid(),
            ProductionFacilityId = facility2.Id,
            EquipmentTypeId = equipment2.Id,
            Quantity = 5
        };

        var contract3 = new PlacementContract
        {
            Id = Guid.NewGuid(),
            ProductionFacilityId = facility3.Id,
            EquipmentTypeId = equipment3.Id,
            Quantity = 15
        };

        var contract4 = new PlacementContract
        {
            Id = Guid.NewGuid(),
            ProductionFacilityId = facility1.Id,
            EquipmentTypeId = equipment4.Id,
            Quantity = 8
        };

        var contract5 = new PlacementContract
        {
            Id = Guid.NewGuid(),
            ProductionFacilityId = facility2.Id,
            EquipmentTypeId = equipment3.Id,
            Quantity = 20
        };

        modelBuilder.Entity<ProductionFacility>().HasData(facility1, facility2, facility3);
        modelBuilder.Entity<EquipmentType>().HasData(equipment1, equipment2, equipment3, equipment4);
        modelBuilder.Entity<PlacementContract>().HasData(contract1, contract2, contract3, contract4, contract5);
    }
}
