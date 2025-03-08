using Meowv.Blog.Application.Dto;
using Meowv.Blog.Application.Dto.Hots;

namespace Meowv.Blog.Application.IServices
{
    public interface IHotCacheService : ICacheRemoveService
    {
        /// <summary>
        /// Get the list of sources from the cache.
        /// </summary>
        /// <param name="func"></param>
        /// <returns></returns>
        Task<BlogResponse<List<HotSourceDto>>> GetSourcesAsync(Func<Task<BlogResponse<List<HotSourceDto>>>> func);

        /// <summary>
        /// Get the list of hot news by id from the cache.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="func"></param>
        /// <returns></returns>
        Task<BlogResponse<HotDto>> GetHotsAsync(string id, Func<Task<BlogResponse<HotDto>>> func);
    }
}