using System.Threading.Tasks;
using Meowv.Blog.Caching;
using Meowv.Blog.Domain.Blog;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Entities.Events;
using Volo.Abp.EventBus;

namespace Meowv.Blog.Application.Blog.EventHandlers;

public class CategoryEventHandler : ILocalEventHandler<EntityCreatedEventData<Category>>,
    ILocalEventHandler<EntityDeletedEventData<Category>>,
    ILocalEventHandler<EntityUpdatedEventData<Category>>,
    ITransientDependency
{
    private readonly IBlogCacheAppService _cacheApp;

    public CategoryEventHandler(IBlogCacheAppService cacheApp)
    {
        _cacheApp = cacheApp;
    }

    public async Task HandleEventAsync(EntityCreatedEventData<Category> eventData)
    {
        await _cacheApp.RemoveAsync(CachingConsts.CachePrefix.Blog_Category);
    }

    public async Task HandleEventAsync(EntityDeletedEventData<Category> eventData)
    {
        await _cacheApp.RemoveAsync(CachingConsts.CachePrefix.Blog_Category);
    }

    public async Task HandleEventAsync(EntityUpdatedEventData<Category> eventData)
    {
        await _cacheApp.RemoveAsync(CachingConsts.CachePrefix.Blog_Category);
    }
}