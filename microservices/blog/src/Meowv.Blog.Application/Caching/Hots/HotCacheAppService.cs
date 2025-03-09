using Meowv.Blog.Application.Dto;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Meowv.Blog.Caching.Hots;

public class HotCacheAppService : CachingServiceBase, IHotCacheAppService
{
    public async Task<BlogResponse<List<HotSourceDto>>> GetSourcesAsync(
        Func<Task<BlogResponse<List<HotSourceDto>>>> func)
    {
        return await Cache.GetOrAddAsync(CachingConsts.CacheKeys.GetSources(), func,
            CachingConsts.CacheStrategy.ONE_HOURS);
    }

    public async Task<BlogResponse<HotDto>> GetHotsAsync(string id, Func<Task<BlogResponse<HotDto>>> func)
    {
        return await Cache.GetOrAddAsync(CachingConsts.CacheKeys.GetHots(id), func,
            CachingConsts.CacheStrategy.ONE_HOURS);
    }
}