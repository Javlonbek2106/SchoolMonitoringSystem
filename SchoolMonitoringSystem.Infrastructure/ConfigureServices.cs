using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SchoolMonitoringSystem.Application.Common;
using SchoolMonitoringSystem.Application.Common.Interfaces;
using SchoolMonitoringSystem.Application.Common.Services;
using SchoolMonitoringSystem.Infrastructure;
using SchoolMonitoringSystem.Infrastructure.Persistence;
using SchoolMonitoringSystem.Infrastructure.Services;
using StudentPaymentSystem.Application.Common.Interfaces;

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
        services.AddScoped<ISaveImg, SaveImg>();
        services.AddScoped<IDeleteImg, DeleteImg>();


        return services;
    }
}
