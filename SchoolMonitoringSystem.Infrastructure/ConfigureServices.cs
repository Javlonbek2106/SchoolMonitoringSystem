using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SchoolMonitoringSystem.Application.Common;
using SchoolMonitoringSystem.Infrastructure.Persistence;
using SchoolMonitoringSystem.Infrastructure.Services;
using StudentPaymentSystem.Application.Common.Interfaces;

namespace SchoolMonitoringSystem.Infrastructure;

public static class ConfigureServices
{
    public static IServiceCollection AddInfrastructureService(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<IApplicationDbContext, ApplicationDbContext>(options =>
        {
            options.UseNpgsql(connectionString: configuration.GetConnectionString("DbConnection"));
        });

        services.AddScoped<AuditableEntitySaveChangesInterceptor>();
        
        services.AddScoped<IApplicationDbContext, ApplicationDbContext>();

        services.AddTransient<IDateTime, DateTimeService>();
        
        services.AddTransient<IGuidGenerator, GuidGeneratorService>();
        
        return services;
    }
}