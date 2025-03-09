using System.Threading.Tasks;
using Meowv.Blog.Caching;
using Meowv.Blog.Domain.Blog;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Entities.Events;
using Volo.Abp.EventBus;

namespace Meowv.Blog.Application.Blog.EventHandlers;

public class TagEventHandler : ILocalEventHandler<EntityCreatedEventData<Tag>>,
    ILocalEventHandler<EntityDeletedEventData<Tag>>,
    ILocalEventHandler<EntityUpdatedEventData<Tag>>,
    ITransientDependency
{
    private readonly IBlogCacheAppService _cacheApp;

    public TagEventHandler(IBlogCacheAppService cacheApp)
    {
        _cacheApp = cacheApp;
    }

    public async Task HandleEventAsync(EntityCreatedEventData<Tag> eventData)
    {
        await _cacheApp.RemoveAsync(CachingConsts.CachePrefix.Blog_Tag);
    }

    public async Task HandleEventAsync(EntityDeletedEventData<Tag> eventData)
    {
        await _cacheApp.RemoveAsync(CachingConsts.CachePrefix.Blog_Tag);
    }

    public async Task HandleEventAsync(EntityUpdatedEventData<Tag> eventData)
    {
        await _cacheApp.RemoveAsync(CachingConsts.CachePrefix.Blog_Tag);
    }
}