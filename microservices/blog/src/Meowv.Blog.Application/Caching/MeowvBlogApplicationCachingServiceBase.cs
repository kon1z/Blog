using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.DependencyInjection;

namespace Meowv.Blog.Caching;

public class CachingServiceBase : ITransientDependency
{
    protected readonly object ServiceProviderLock = new();

    private IDistributedCache _cache;

    public IServiceProvider ServiceProvider { get; set; }

    protected IDistributedCache Cache => LazyGetRequiredService(ref _cache);

    protected TService LazyGetRequiredService<TService>(ref TService reference)
    {
        return LazyGetRequiredService(typeof(TService), ref reference);
    }

    protected TRef LazyGetRequiredService<TRef>(Type serviceType, ref TRef reference)
    {
        if (reference == null)
            lock (ServiceProviderLock)
            {
                if (reference == null) reference = (TRef)ServiceProvider.GetRequiredService(serviceType);
            }

        return reference;
    }

    public Task RemoveAsync(string key)
    {
        return Task.CompletedTask;
    }
}