using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hangfire;
using Hangfire.PostgreSql;
using Hangfire.SqlServer;
using Hangfire.Storage;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ramsha.Application.Contracts.BackgroundJobs;
using Ramsha.BackgroundJobs.Services;

namespace Ramsha.BackgroundJobs;

public static class RegisterServices
{
    public static IServiceCollection AddBackgroundJobsServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHangfire(x => x.UsePostgreSqlStorage(
            configuration.GetConnectionString("DefaultConnection"),
            new PostgreSqlStorageOptions
            {
                SchemaName = "BackgroundJobs",
            }));

        services.AddTransient<IBackgroundJobService, BackgroundJobService>();

        services.AddHangfireServer();

        return services;

    }
}
