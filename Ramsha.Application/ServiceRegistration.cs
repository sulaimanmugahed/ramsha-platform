using FluentValidation;
using Ramsha.Application.Contracts;
using Ramsha.Application.Services;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Ramsha.Domain.Settings;
using Microsoft.Extensions.Configuration;
using Ramsha.Contracts;


namespace Ramsha.Application;

public static class ServiceRegistration
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        services.AddScoped<VariantService>();
        services.AddScoped<ICodeGenerator, CodeGenerator>();

        services.Configure<GlobalAppSettings>(configuration.GetSection(nameof(GlobalAppSettings)));
        services.AddScoped<DeliveryFeeService>();
        services.AddScoped<BasketService>();

        services.AddScoped<IGeocodingService, GeocodingService>();



        return services;

    }
}

