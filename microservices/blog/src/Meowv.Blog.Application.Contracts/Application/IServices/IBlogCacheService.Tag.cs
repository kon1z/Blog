using Meowv.Blog.Application.Dto;
using Meowv.Blog.Application.Dto.Blog;

namespace Meowv.Blog.Application.IServices
{
    public partial interface IBlogCacheService
    {
        /// <summary>
        /// Get the list of tags from the cache.
        /// </summary>
        /// <param name="func"></param>
        /// <returns></returns>
        Task<BlogResponse<List<GetTagDto>>> GetTagsAsync(Func<Task<BlogResponse<List<GetTagDto>>>> func);
    }
}