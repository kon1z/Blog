using Meowv.Blog.Application.Dto;

namespace Meowv.Blog.Caching;

public interface IHotCacheAppService : ICacheRemoveService
{
    /// <summary>
    ///     Get the list of sources from the cache.
    /// </summary>
    /// <param name="func"></param>
    /// <returns></returns>
    Task<BlogResponse<List<HotSourceDto>>> GetSourcesAsync(Func<Task<BlogResponse<List<HotSourceDto>>>> func);

    /// <summary>
    ///     Get the list of hot news by id from the cache.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="func"></param>
    /// <returns></returns>
    Task<BlogResponse<HotDto>> GetHotsAsync(string id, Func<Task<BlogResponse<HotDto>>> func);
}