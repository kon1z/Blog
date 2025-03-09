using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Volo.Abp;

namespace Meowv.DbMigrator;

public class DbMigratorHostedService : IHostedService
{
    private readonly IHostApplicationLifetime _hostApplicationLifetime;

    public DbMigratorHostedService(IHostApplicationLifetime hostApplicationLifetime)
    {
        _hostApplicationLifetime = hostApplicationLifetime;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        using var application =
            AbpApplicationFactory.Create<MeowvDbMigratorModule>(options => { options.UseAutofac(); });
        application.Initialize();

        Console.WriteLine("Executing database seed.");

        // TODO 重构种子数据

        Console.WriteLine("Successfully completed database seed.");

        application.Shutdown();
        _hostApplicationLifetime.StopApplication();
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}