﻿using Microsoft.EntityFrameworkCore;
using Test_Smart.BackgroundServices;
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
        AddLogging(builder);
    }

    private static void AddDbContext(IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(connectionString));
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
                    Array.Empty<string>() 
                }
            });
        });
    }
    public static void AddServices(IServiceCollection services)
    {
        services.AddScoped<IPlacementContractService, PlacementContractService>();
        services.AddScoped<IEquipmentTypeService, EquipmentTypeService>();
        services.AddScoped<IProductionFacilityService, ProductionFacilityService>();
    
    }
    private static void AddInfrastructure(IServiceCollection services)
    {
        services.AddAuthorization();
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        
        services.AddSingleton<LoggerBackgroundService>();
        services.AddHostedService(provider => provider.GetRequiredService<LoggerBackgroundService>());
    }

    private static void AddLogging(WebApplicationBuilder builder)
    {
        builder.Logging.ClearProviders();
        builder.Logging.AddConsole();
        builder.Logging.AddDebug();
        builder.Logging.SetMinimumLevel(LogLevel.Information);

    }
}
