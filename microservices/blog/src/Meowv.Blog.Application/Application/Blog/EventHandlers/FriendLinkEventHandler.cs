using System.Threading.Tasks;
using Meowv.Blog.Caching;
using Meowv.Blog.Domain.Blog;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Entities.Events;
using Volo.Abp.EventBus;

namespace Meowv.Blog.Application.Blog.EventHandlers;

public class FriendLinkEventHandler : ILocalEventHandler<EntityCreatedEventData<FriendLink>>,
    ILocalEventHandler<EntityDeletedEventData<FriendLink>>,
    ILocalEventHandler<EntityUpdatedEventData<FriendLink>>,
    ITransientDependency
{
    private readonly IBlogCacheAppService _cacheApp;

    public FriendLinkEventHandler(IBlogCacheAppService cacheApp)
    {
        _cacheApp = cacheApp;
    }

    public async Task HandleEventAsync(EntityCreatedEventData<FriendLink> eventData)
    {
        await _cacheApp.RemoveAsync(CachingConsts.CachePrefix.Blog_FriendLink);
    }

    public async Task HandleEventAsync(EntityDeletedEventData<FriendLink> eventData)
    {
        await _cacheApp.RemoveAsync(CachingConsts.CachePrefix.Blog_FriendLink);
    }

    public async Task HandleEventAsync(EntityUpdatedEventData<FriendLink> eventData)
    {
        await _cacheApp.RemoveAsync(CachingConsts.CachePrefix.Blog_FriendLink);
    }
}