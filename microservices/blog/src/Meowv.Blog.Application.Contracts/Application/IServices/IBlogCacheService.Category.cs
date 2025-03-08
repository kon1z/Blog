using Meowv.Blog.Application.Dto;
using Meowv.Blog.Application.Dto.Blog;

namespace Meowv.Blog.Application.IServices
{
    public partial interface IBlogCacheService
    {
        /// <summary>
        /// Get the list of categories from the cache.
        /// </summary>
        /// <param name="func"></param>
        /// <returns></returns>
        Task<BlogResponse<List<GetCategoryDto>>> GetCategoriesAsync(Func<Task<BlogResponse<List<GetCategoryDto>>>> func);
    }
}