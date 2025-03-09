using System.Threading.Tasks;
using Meowv.Blog.Caching;
using Meowv.Blog.EventData.Hots;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EventBus;

namespace Meowv.Blog.Application.Hots.EventHandlers;

public class HotEventHandler : ILocalEventHandler<HotWorkerEventData>, ITransientDependency
{
    private readonly IHotCacheAppService _cacheApp;

    public HotEventHandler(IHotCacheAppService cacheApp)
    {
        _cacheApp = cacheApp;
    }

    public async Task HandleEventAsync(HotWorkerEventData eventData)
    {
        await _cacheApp.RemoveAsync(CachingConsts.CachePrefix.Hot);
    }
}