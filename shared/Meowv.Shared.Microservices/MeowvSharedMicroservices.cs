using Medallion.Threading;
using Medallion.Threading.Redis;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;
using Volo.Abp.BackgroundJobs.RabbitMQ;
using Volo.Abp.Caching;
using Volo.Abp.Caching.StackExchangeRedis;
using Volo.Abp.DistributedLocking;
using Volo.Abp.EventBus.RabbitMq;
using Volo.Abp.Modularity;

namespace Meowv;

[DependsOn(typeof(MeowvSharedAspnetCoreSharedModule),
    typeof(AbpBackgroundJobsRabbitMqModule),
    typeof(AbpEventBusRabbitMqModule),
    typeof(AbpCachingStackExchangeRedisModule),
    typeof(AbpDistributedLockingModule)
)]
public class MeowvSharedMicroservices : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        var configuration = context.Services.GetConfiguration();

        Configure<AbpDistributedCacheOptions>(options => { options.KeyPrefix = "Meowv:"; });

        var redis = ConnectionMultiplexer.Connect(configuration["Redis:Configuration"]!);
        context.Services
            .AddDataProtection()
            .PersistKeysToStackExchangeRedis(redis, "Meowv-Protection-Keys");

        context.Services.AddSingleton<IDistributedLockProvider>(sp =>
        {
            var connection = ConnectionMultiplexer.Connect(configuration["Redis:Configuration"]!);
            return new RedisDistributedSynchronizationProvider(connection.GetDatabase());
        });
    }
}