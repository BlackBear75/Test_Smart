using Microsoft.EntityFrameworkCore;
using Test_Smart.Base.Repository;
using Test_Smart.Entity.EquipmentType.Repository;
using Test_Smart.Entity.PlacementContract.Repository;
using Test_Smart.Entity.ProductionFacility.Repository;
using Test_Smart.Service;

namespace Test_Smart.Configuration;

public static class DependencyStartup
{
    public static void ConfigureServices(this WebApplicationBuilder builder)
    {
        AddDbContext(builder.Services, builder.Configuration);
        AddRepositories(builder.Services);
        AddInfrastructure(builder.Services);
        AddServices(builder.Services);
        AddSwaggerGen(builder.Services);
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

    public static void AddSwaggerGen(IServiceCollection services)
    {
        services.AddSwaggerGen(c =>
        {
            c.AddSecurityDefinition("ApiKey", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Description = "API Key must be provided in the header",
                Name = "X-Api-Key",
                In = Microsoft.OpenApi.Models.ParameterLocation.Header,
                Type = Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey,
                Scheme = "ApiKey"
            });

            c.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
            {
                {
                    new Microsoft.OpenApi.Models.OpenApiSecurityScheme
                    {
                        Reference = new Microsoft.OpenApi.Models.OpenApiReference
                        {
                            Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                            Id = "ApiKey"
                        }
                    },
                    Array.Empty<string>() // Порожній масив, бо ключ не залежить від ролей
                }
            });
        });
    }
    public static void AddServices(IServiceCollection services)
    {
        services.AddScoped<IPlacementContractService, PlacementContractService>();
    
    }
    private static void AddInfrastructure(IServiceCollection services)
    {
        services.AddAuthorization();
        services.AddControllers();
        services.AddEndpointsApiExplorer();
    }
}
