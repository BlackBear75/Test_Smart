using Microsoft.EntityFrameworkCore;
using Test_Smart.Base.Repository;
using Test_Smart.Entity.EquipmentType.Repository;
using Test_Smart.Entity.PlacementContract.Repository;
using Test_Smart.Entity.ProductionFacility.Repository;

namespace Test_Smart.Configuration;

public static class DependencyStartup
{
    public static void ConfigureServices(this WebApplicationBuilder builder)
    {
        AddDbContext(builder.Services, builder.Configuration);
        AddRepositories(builder.Services);
        AddInfrastructure(builder.Services);
    }

    private static void AddDbContext(IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
    }

    private static void AddRepositories(IServiceCollection services)
    {
        services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
        
        services.AddScoped(typeof(IProductionFacilityRepository<>), typeof(ProductionFacilityRepository<>));
        services.AddScoped(typeof(IPlacementContractRepository<>), typeof(PlacementContractRepositoryRepository<>));
        services.AddScoped(typeof(IEquipmentTypeRepository<>), typeof(EquipmentTypeRepository<>));
     
    }


    private static void AddInfrastructure(IServiceCollection services)
    {
        services.AddAuthorization();
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
    }
}
