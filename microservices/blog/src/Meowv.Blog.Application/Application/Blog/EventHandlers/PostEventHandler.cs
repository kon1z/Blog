﻿using System.Threading.Tasks;
using Meowv.Blog.Caching;
using Meowv.Blog.Domain.Blog;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Entities.Events;
using Volo.Abp.EventBus;

namespace Meowv.Blog.Application.Blog.EventHandlers;

public class PostEventHandler : ILocalEventHandler<EntityCreatedEventData<Post>>,
    ILocalEventHandler<EntityDeletedEventData<Post>>,
    ILocalEventHandler<EntityUpdatedEventData<Post>>,
    ITransientDependency
{
    private readonly IBlogCacheAppService _cacheApp;

    public PostEventHandler(IBlogCacheAppService cacheApp)
    {
        _cacheApp = cacheApp;
    }

    public async Task HandleEventAsync(EntityCreatedEventData<Post> eventData)
    {
        await _cacheApp.RemoveAsync(CachingConsts.CachePrefix.Blog_Post);
    }

    public async Task HandleEventAsync(EntityDeletedEventData<Post> eventData)
    {
        await _cacheApp.RemoveAsync(CachingConsts.CachePrefix.Blog_Post);
    }

    public async Task HandleEventAsync(EntityUpdatedEventData<Post> eventData)
    {
        await _cacheApp.RemoveAsync(CachingConsts.CachePrefix.Blog_Post);
    }
}