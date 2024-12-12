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
       
       
     

}