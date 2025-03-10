using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Meowv.Blog.Application.Dto;

namespace Meowv.Blog.Caching.Blog;

public class BlogCacheAppService : CachingServiceBase, IBlogCacheAppService
{
    public async Task<BlogResponse<List<GetTagDto>>> GetTagsAsync(Func<Task<BlogResponse<List<GetTagDto>>>> func)
    {
        return await Cache.GetOrAddAsync(CachingConsts.CacheKeys.GetTags(), func, CachingConsts.CacheStrategy.HALF_DAY);
    }

    public async Task<BlogResponse<PostDetailDto>> GetPostByUrlAsync(string url,
        Func<Task<BlogResponse<PostDetailDto>>> func)
    {
        return await Cache.GetOrAddAsync(CachingConsts.CacheKeys.GetPostByUrl(url), func,
            CachingConsts.CacheStrategy.HALF_DAY);
    }

    public async Task<BlogResponse<PagedList<GetPostDto>>> GetPostsAsync(int page, int limit,
        Func<Task<BlogResponse<PagedList<GetPostDto>>>> func)
    {
        return await Cache.GetOrAddAsync(CachingConsts.CacheKeys.GetPosts(page, limit), func,
            CachingConsts.CacheStrategy.HALF_DAY);
    }

    public async Task<BlogResponse<List<GetPostDto>>> GetPostsByCategoryAsync(string category,
        Func<Task<BlogResponse<List<GetPostDto>>>> func)
    {
        return await Cache.GetOrAddAsync(CachingConsts.CacheKeys.GetPostsByCategory(category), func,
            CachingConsts.CacheStrategy.HALF_DAY);
    }

    public async Task<BlogResponse<List<GetPostDto>>> GetPostsByTagAsync(string tag,
        Func<Task<BlogResponse<List<GetPostDto>>>> func)
    {
        return await Cache.GetOrAddAsync(CachingConsts.CacheKeys.GetPostsByTag(tag), func,
            CachingConsts.CacheStrategy.HALF_DAY);
    }

    public async Task<BlogResponse<List<FriendLinkDto>>> GetFriendLinksAsync(
        Func<Task<BlogResponse<List<FriendLinkDto>>>> func)
    {
        return await Cache.GetOrAddAsync(CachingConsts.CacheKeys.GetFriendLinks(), func,
            CachingConsts.CacheStrategy.HALF_DAY);
    }

    public async Task<BlogResponse<List<GetCategoryDto>>> GetCategoriesAsync(
        Func<Task<BlogResponse<List<GetCategoryDto>>>> func)
    {
        return await Cache.GetOrAddAsync(CachingConsts.CacheKeys.GetCategories(), func,
            CachingConsts.CacheStrategy.HALF_DAY);
    }
}