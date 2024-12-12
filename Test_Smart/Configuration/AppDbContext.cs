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

        var productionFacility1 = new ProductionFacility
        {
            Id = Guid.NewGuid(),
            Code = "FAC001",
            Name = "Factory A",
            StandardArea = 1000
        };

        var productionFacility2 = new ProductionFacility
        {
            Id = Guid.NewGuid(),
            Code = "FAC002", 
            Name = "Factory B",
            StandardArea = 800
        };

        var equipmentType1 = new EquipmentType
        {
            Id = Guid.NewGuid(),
            Code = "EQ001", 
            Name = "Machine A",
            AreaPerUnit = 50
        };

        var equipmentType2 = new EquipmentType
        {
            Id = Guid.NewGuid(),
            Code = "EQ002", 
            Name = "Machine B",
            AreaPerUnit = 70
        };

        modelBuilder.Entity<ProductionFacility>().HasData(productionFacility1, productionFacility2);
        modelBuilder.Entity<EquipmentType>().HasData(equipmentType1, equipmentType2);

        modelBuilder.Entity<PlacementContract>().HasData(
            new PlacementContract
            {
                Id = Guid.NewGuid(),
                ProductionFacilityId = productionFacility1.Id,
                EquipmentTypeId = equipmentType1.Id,
                Quantity = 10
            },
            new PlacementContract
            {
                Id = Guid.NewGuid(),
                ProductionFacilityId = productionFacility2.Id,
                EquipmentTypeId = equipmentType2.Id,
                Quantity = 5
            }
        );
    }
}
