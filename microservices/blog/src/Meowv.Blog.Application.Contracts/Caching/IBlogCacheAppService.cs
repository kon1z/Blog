using Meowv.Blog.Application.Dto;

namespace Meowv.Blog.Caching;

public interface IBlogCacheAppService : ICacheRemoveService
{
    /// <summary>
    ///     Get the list of categories from the cache.
    /// </summary>
    /// <param name="func"></param>
    /// <returns></returns>
    Task<BlogResponse<List<GetCategoryDto>>> GetCategoriesAsync(Func<Task<BlogResponse<List<GetCategoryDto>>>> func);
    /// <summary>
    ///     Get the list of friendlinks from the cache.
    /// </summary>
    /// <param name="func"></param>
    /// <returns></returns>
    Task<BlogResponse<List<FriendLinkDto>>> GetFriendLinksAsync(Func<Task<BlogResponse<List<FriendLinkDto>>>> func);
    /// <summary>
    ///     Get post by url from the cache.
    /// </summary>
    /// <param name="url"></param>
    /// <param name="func"></param>
    /// <returns></returns>
    Task<BlogResponse<PostDetailDto>> GetPostByUrlAsync(string url, Func<Task<BlogResponse<PostDetailDto>>> func);

    /// <summary>
    ///     Get the list of posts by paging from the cache.
    /// </summary>
    /// <param name="page"></param>
    /// <param name="limit"></param>
    /// <param name="func"></param>
    /// <returns></returns>
    Task<BlogResponse<PagedList<GetPostDto>>> GetPostsAsync(int page, int limit,
        Func<Task<BlogResponse<PagedList<GetPostDto>>>> func);

    /// <summary>
    ///     Get the list of posts by category from the cache.
    /// </summary>
    /// <param name="category"></param>
    /// <param name="func"></param>
    /// <returns></returns>
    Task<BlogResponse<List<GetPostDto>>> GetPostsByCategoryAsync(string category,
        Func<Task<BlogResponse<List<GetPostDto>>>> func);

    /// <summary>
    ///     Get the list of posts by tag from the cache.
    /// </summary>
    /// <param name="tag"></param>
    /// <param name="func"></param>
    /// <returns></returns>
    Task<BlogResponse<List<GetPostDto>>>
        GetPostsByTagAsync(string tag, Func<Task<BlogResponse<List<GetPostDto>>>> func);
    /// <summary>
    ///     Get the list of tags from the cache.
    /// </summary>
    /// <param name="func"></param>
    /// <returns></returns>
    Task<BlogResponse<List<GetTagDto>>> GetTagsAsync(Func<Task<BlogResponse<List<GetTagDto>>>> func);
}