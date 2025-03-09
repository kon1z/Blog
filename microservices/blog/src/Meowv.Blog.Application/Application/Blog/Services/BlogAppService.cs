using Meowv.Blog.Application.Dto;
using Meowv.Blog.Application.IServices;
using Meowv.Blog.Caching;
using Meowv.Blog.Domain.Blog;
using Meowv.Blog.Domain.Blog.Repositories;
using Meowv.Blog.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Meowv.Blog.Application.Blog.Services;

public class BlogAppService : ServiceBase, IBlogAppService
{
    private readonly IBlogCacheAppService _cacheApp;
    private readonly ICategoryRepository _categories;
    private readonly IFriendLinkRepository _friendLinks;
    private readonly IPostRepository _posts;
    private readonly ITagRepository _tags;

    public BlogAppService(IPostRepository posts,
        ICategoryRepository categories,
        ITagRepository tags,
        IFriendLinkRepository friendLinks,
        IBlogCacheAppService cacheApp)
    {
        _posts = posts;
        _categories = categories;
        _tags = tags;
        _friendLinks = friendLinks;
        _cacheApp = cacheApp;
    }

    /// <summary>
    ///     Get statistics on the total number of posts, categories and tags.
    /// </summary>
    /// <returns></returns>
    [Route("api/meowv/blog/statistics")]
    public async Task<BlogResponse<Tuple<int, int, int>>> GetStatisticsAsync()
    {
        var response = new BlogResponse<Tuple<int, int, int>>();

        var postCount = await _posts.GetCountAsync();
        var categoryCount = await _categories.GetCountAsync();
        var tagCount = await _tags.GetCountAsync();

        response.Result = new Tuple<int, int, int>(AbpObjectExtensions.To<int>(postCount), AbpObjectExtensions.To<int>(categoryCount), AbpObjectExtensions.To<int>(tagCount));
        return response;
    }


    /// <summary>
    ///     Get post by url.
    /// </summary>
    /// <param name="url"></param>
    /// <returns></returns>
    [Route("api/meowv/blog/post")]
    public async Task<BlogResponse<PostDetailDto>> GetPostByUrlAsync(string url)
    {
        return await _cacheApp.GetPostByUrlAsync(url, async () =>
        {
            var response = new BlogResponse<PostDetailDto>();

            var post = await _posts.FindAsync(x => x.Url == url);
            if (post is null)
            {
                response.IsFailed("The post url not exists.");
                return response;
            }

            var previous = await _posts.FirstOrDefaultAsync(x => x.CreatedAt > post.CreatedAt);
            var next = await _posts.FirstOrDefaultAsync(x => x.CreatedAt < post.CreatedAt);

            var result = ObjectMapper.Map<Post, PostDetailDto>(post);
            result.Previous = new PostPagedDto(previous.Title, previous.Url);
            result.Next = new PostPagedDto(next.Title, next.Url);

            response.Result = result;
            return response;
        });
    }

    /// <summary>
    ///     Get the list of posts by paging.
    /// </summary>
    /// <param name="page"></param>
    /// <param name="limit"></param>
    /// <returns></returns>
    [Route("api/meowv/blog/posts/{page}/{limit}")]
    public async Task<BlogResponse<PagedList<GetPostDto>>> GetPostsAsync([Range(1, 100)] int page = 1,
        [Range(10, 100)] int limit = 10)
    {
        return await _cacheApp.GetPostsAsync(page, limit, async () =>
        {
            var response = new BlogResponse<PagedList<GetPostDto>>();

            var result = await _posts.GetPagedListAsync(page, limit);
            var total = result.Item1;
            var posts = GetPostList(result.Item2);

            response.Result = new PagedList<GetPostDto>(total, posts);
            return response;
        });
    }

    /// <summary>
    ///     Get the list of posts by category.
    /// </summary>
    /// <param name="category"></param>
    /// <returns></returns>
    [Route("api/meowv/blog/posts/category/{category}")]
    public async Task<BlogResponse<List<GetPostDto>>> GetPostsByCategoryAsync(string category)
    {
        return await _cacheApp.GetPostsByCategoryAsync(category, async () =>
        {
            var response = new BlogResponse<List<GetPostDto>>();

            var entity = await _categories.FindAsync(x => x.Alias == category);
            if (entity is null)
            {
                response.IsFailed($"The category:{category} not exists.");
                return response;
            }

            var posts = await _posts.GetListByCategoryAsync(category);

            response.IsSuccess(GetPostList(posts), entity.Name);
            return response;
        });
    }

    /// <summary>
    ///     Get the list of posts by tag.
    /// </summary>
    /// <param name="tag"></param>
    /// <returns></returns>
    [Route("api/meowv/blog/posts/tag/{tag}")]
    public async Task<BlogResponse<List<GetPostDto>>> GetPostsByTagAsync(string tag)
    {
        return await _cacheApp.GetPostsByTagAsync(tag, async () =>
        {
            var response = new BlogResponse<List<GetPostDto>>();

            var entity = await _tags.FindAsync(x => x.Alias == tag);
            if (entity is null)
            {
                response.IsFailed($"The tag:{tag} not exists.");
                return response;
            }

            var posts = await _posts.GetListByTagAsync(tag);

            response.IsSuccess(GetPostList(posts), entity.Name);
            return response;
        });
    }

    private List<GetPostDto> GetPostList(List<Post> posts)
    {
        return ObjectMapper.Map<List<Post>, List<PostBriefDto>>(posts)
            .GroupBy(x => x.Year)
            .Select(x => new GetPostDto
            {
                Year = x.Key,
                Posts = x
            }).ToList();
    }

    /// <summary>
    ///     Create a tag.
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [Authorize]
    [Route("api/meowv/blog/tag")]
    public async Task<BlogResponse> CreateTagAsync(CreateTagInput input)
    {
        var response = new BlogResponse();

        var tag = await _tags.FindAsync(x => x.Name == input.Name);
        if (tag is not null)
        {
            response.IsFailed($"The tag:{input.Name} already exists.");
            return response;
        }

        await _tags.InsertAsync(new Tag
        {
            Name = input.Name,
            Alias = input.Alias
        });

        return response;
    }

    /// <summary>
    ///     Delete tag by id.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [Authorize]
    [Route("api/meowv/blog/tag/{id}")]
    public async Task<BlogResponse> DeleteTagAsync(string id)
    {
        var response = new BlogResponse();

        var tag = await _tags.FindAsync(id.ToObjectId());
        if (tag is null)
        {
            response.IsFailed("The tag id not exists.");
            return response;
        }

        await _tags.DeleteAsync(id.ToObjectId());

        return response;
    }

    /// <summary>
    ///     Update tag by id.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="input"></param>
    /// <returns></returns>
    [Authorize]
    [Route("api/meowv/blog/tag/{id}")]
    public async Task<BlogResponse> UpdateTagAsync(string id, UpdateTagInput input)
    {
        var response = new BlogResponse();

        var tag = await _tags.FindAsync(id.ToObjectId());
        if (tag is null)
        {
            response.IsFailed("The tag id not exists.");
            return response;
        }

        tag.Name = input.Name;
        tag.Alias = input.Alias;

        await _tags.UpdateAsync(tag);

        return response;
    }

    /// <summary>
    ///     Get the list of tags.
    /// </summary>
    /// <returns></returns>
    [Authorize]
    [Route("api/meowv/blog/admin/tags")]
    public async Task<BlogResponse<List<GetAdminTagDto>>> GetAdminTagsAsync()
    {
        var response = new BlogResponse<List<GetAdminTagDto>>();

        var tags = await _tags.GetListAsync();

        var result = ObjectMapper.Map<List<Tag>, List<GetAdminTagDto>>(tags);
        result.ForEach(x => { x.Total = _posts.GetCountByTagAsync(x.Id.ToObjectId()).Result; });

        response.Result = result;
        return response;
    }

    /// <summary>
    ///     Create a post.
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [Authorize]
    [Route("api/meowv/blog/post")]
    public async Task<BlogResponse> CreatePostAsync(CreatePostInput input)
    {
        var response = new BlogResponse();

        var tags = await _tags.GetListAsync();
        var newTags = input.Tags.Where(item => !tags.Any(x => x.Name == item)).Select(x => new Tag
        {
            Name = x,
            Alias = x.ToLower()
        });
        if (newTags.Any()) await _tags.InsertManyAsync(newTags);

        var post = new Post
        {
            Title = input.Title,
            Author = input.Author,
            Url = input.Url.GeneratePostUrl(input.CreatedAt.ToDateTime()),
            Markdown = input.Markdown,
            Category = await _categories.GetAsync(input.CategoryId.ToObjectId()),
            Tags = await _tags.GetListAsync(input.Tags),
            CreatedAt = input.CreatedAt.ToDateTime()
        };
        await _posts.InsertAsync(post);

        return response;
    }

    /// <summary>
    ///     Delete post by id.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [Authorize]
    [Route("api/meowv/blog/post/{id}")]
    public async Task<BlogResponse> DeletePostAsync(string id)
    {
        var response = new BlogResponse();

        var post = await _posts.FindAsync(id.ToObjectId());
        if (post is null)
        {
            response.IsFailed("The post id not exists.");
            return response;
        }

        await _posts.DeleteAsync(id.ToObjectId());

        return response;
    }

    /// <summary>
    ///     Update post by id.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="input"></param>
    /// <returns></returns>
    [Authorize]
    [Route("api/meowv/blog/post/{id}")]
    public async Task<BlogResponse> UpdatePostAsync(string id, UpdatePostInput input)
    {
        var response = new BlogResponse();

        var post = await _posts.FindAsync(id.ToObjectId());
        if (post is null)
        {
            response.IsFailed("The post id not exists.");
            return response;
        }

        var tags = await _tags.GetListAsync();
        var newTags = input.Tags.Where(item => !tags.Any(x => x.Name == item)).Select(x => new Tag
        {
            Name = x,
            Alias = x.ToLower()
        });
        if (newTags.Any()) await _tags.InsertManyAsync(newTags);

        post.Title = input.Title;
        post.Author = input.Author;
        post.Url = input.Url.GeneratePostUrl(input.CreatedAt.ToDateTime());
        post.Markdown = input.Markdown;
        post.Category = await _categories.GetAsync(input.CategoryId.ToObjectId());
        post.Tags = await _tags.GetListAsync(input.Tags);
        post.CreatedAt = input.CreatedAt.ToDateTime();
        await _posts.UpdateAsync(post);

        return response;
    }

    /// <summary>
    ///     Get post by id.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [Authorize]
    [Route("api/meowv/blog/post/{id}")]
    public async Task<BlogResponse<PostDto>> GetPostAsync(string id)
    {
        var response = new BlogResponse<PostDto>();

        var post = await _posts.FindAsync(id.ToObjectId());
        if (post is null)
        {
            response.IsFailed("The post id not exists.");
            return response;
        }

        var result = ObjectMapper.Map<Post, PostDto>(post);
        result.Url = result.Url.Split("-").Last();

        response.Result = result;
        return response;
    }

    /// <summary>
    ///     Get the list of posts by paging.
    /// </summary>
    /// <param name="page"></param>
    /// <param name="limit"></param>
    /// <returns></returns>
    [Authorize]
    [Route("api/meowv/blog/admin/posts/{page}/{limit}")]
    public async Task<BlogResponse<PagedList<GetAdminPostDto>>> GetAdminPostsAsync([Range(1, 100)] int page = 1,
        [Range(10, 100)] int limit = 10)
    {
        var response = new BlogResponse<PagedList<GetAdminPostDto>>();

        var result = await _posts.GetPagedListAsync(page, limit);
        var total = result.Item1;
        var posts = ObjectMapper.Map<List<Post>, List<GetAdminPostDto>>(result.Item2);

        response.Result = new PagedList<GetAdminPostDto>(total, posts);
        return response;
    }

    /// <summary>
    ///     Create a friendLink.
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [Authorize]
    [Route("api/meowv/blog/friendlink")]
    public async Task<BlogResponse> CreateFriendLinkAsync(CreateFriendLinkInput input)
    {
        var response = new BlogResponse();

        var friendLink = await _friendLinks.FindAsync(x => x.Name == input.Name);
        if (friendLink is not null)
        {
            response.IsFailed($"The friendLink:{input.Name} already exists.");
            return response;
        }

        await _friendLinks.InsertAsync(new FriendLink
        {
            Name = input.Name,
            Url = input.Url
        });

        return response;
    }

    /// <summary>
    ///     Delete friendLink by id.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [Authorize]
    [Route("api/meowv/blog/friendlink/{id}")]
    public async Task<BlogResponse> DeleteFriendLinkAsync(string id)
    {
        var response = new BlogResponse();

        var friendLink = await _friendLinks.FindAsync(id.ToObjectId());
        if (friendLink is null)
        {
            response.IsFailed("The friendLink id not exists.");
            return response;
        }

        await _friendLinks.DeleteAsync(id.ToObjectId());

        return response;
    }

    /// <summary>
    ///     Update friendLink by id.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="input"></param>
    /// <returns></returns>
    [Authorize]
    [Route("api/meowv/blog/friendlink/{id}")]
    public async Task<BlogResponse> UpdateFriendLinkAsync(string id, UpdateFriendLinkInput input)
    {
        var response = new BlogResponse();

        var friendLink = await _friendLinks.FindAsync(id.ToObjectId());
        if (friendLink is null)
        {
            response.IsFailed("The friendLink id not exists.");
            return response;
        }

        friendLink.Name = input.Name;
        friendLink.Url = input.Url;

        await _friendLinks.UpdateAsync(friendLink);

        return response;
    }

    /// <summary>
    ///     Get the list of friendlinks.
    /// </summary>
    /// <returns></returns>
    [Authorize]
    [Route("api/meowv/blog/admin/friendlinks")]
    public async Task<BlogResponse<List<GetAdminFriendLinkDto>>> GetAdminFriendLinksAsync()
    {
        var response = new BlogResponse<List<GetAdminFriendLinkDto>>();

        var friendLinks = await _friendLinks.GetListAsync();

        var result = ObjectMapper.Map<List<FriendLink>, List<GetAdminFriendLinkDto>>(friendLinks);

        response.Result = result;
        return response;
    }

    /// <summary>
    ///     Create a category.
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [Authorize]
    [Route("api/meowv/blog/category")]
    public async Task<BlogResponse> CreateCategoryAsync(CreateCategoryInput input)
    {
        var response = new BlogResponse();

        var category = await _categories.FindAsync(x => x.Name == input.Name);
        if (category is not null)
        {
            response.IsFailed($"The category:{input.Name} already exists.");
            return response;
        }

        await _categories.InsertAsync(new Category
        {
            Name = input.Name,
            Alias = input.Alias
        });

        return response;
    }

    /// <summary>
    ///     Delete category by id.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [Authorize]
    [Route("api/meowv/blog/category/{id}")]
    public async Task<BlogResponse> DeleteCategoryAsync(string id)
    {
        var response = new BlogResponse();

        var category = await _categories.FindAsync(id.ToObjectId());
        if (category is null)
        {
            response.IsFailed("The category id not exists.");
            return response;
        }

        await _categories.DeleteAsync(id.ToObjectId());

        return response;
    }

    /// <summary>
    ///     Update category by id.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="input"></param>
    /// <returns></returns>
    [Authorize]
    [Route("api/meowv/blog/category/{id}")]
    public async Task<BlogResponse> UpdateCategoryAsync(string id, UpdateCategoryInput input)
    {
        var response = new BlogResponse();

        var category = await _categories.FindAsync(id.ToObjectId());
        if (category is null)
        {
            response.IsFailed("The category id not exists.");
            return response;
        }

        category.Name = input.Name;
        category.Alias = input.Alias;

        await _categories.UpdateAsync(category);

        return response;
    }

    /// <summary>
    ///     Get the list of categories.
    /// </summary>
    /// <returns></returns>
    [Authorize]
    [Route("api/meowv/blog/admin/categories")]
    public async Task<BlogResponse<List<GetAdminCategoryDto>>> GetAdminCategoriesAsync()
    {
        var response = new BlogResponse<List<GetAdminCategoryDto>>();

        var categories = await _categories.GetListAsync();

        var result = ObjectMapper.Map<List<Category>, List<GetAdminCategoryDto>>(categories);
        result.ForEach(x => { x.Total = _posts.GetCountByCategoryAsync(x.Id.ToObjectId()).Result; });

        response.Result = result;
        return response;
    }

    /// <summary>
    ///     Get the list of friendlinks.
    /// </summary>
    /// <returns></returns>
    [Route("api/meowv/blog/friendlinks")]
    public async Task<BlogResponse<List<FriendLinkDto>>> GetFriendLinksAsync()
    {
        return await _cacheApp.GetFriendLinksAsync(async () =>
        {
            var response = new BlogResponse<List<FriendLinkDto>>();

            var friendLinks = await _friendLinks.GetListAsync();

            var result = ObjectMapper.Map<List<FriendLink>, List<FriendLinkDto>>(friendLinks);

            response.Result = result;
            return response;
        });
    }

    /// <summary>
    ///     Get the list of categories.
    /// </summary>
    /// <returns></returns>
    [Route("api/meowv/blog/categories")]
    public async Task<BlogResponse<List<GetCategoryDto>>> GetCategoriesAsync()
    {
        return await _cacheApp.GetCategoriesAsync(async () =>
        {
            var response = new BlogResponse<List<GetCategoryDto>>();

            var categories = await _categories.GetListAsync();

            var result = categories.Select(x => new GetCategoryDto
            {
                Name = x.Name,
                Alias = x.Alias,
                Total = _posts.GetCountByCategoryAsync(x.Id).Result
            }).Where(x => x.Total > 0).ToList();

            response.Result = result;
            return response;
        });
    }

    /// <summary>
    ///     Get the list of tags.
    /// </summary>
    /// <returns></returns>
    [Route("api/meowv/blog/tags")]
    public async Task<BlogResponse<List<GetTagDto>>> GetTagsAsync()
    {
        return await _cacheApp.GetTagsAsync(async () =>
        {
            var response = new BlogResponse<List<GetTagDto>>();

            var tags = await _tags.GetListAsync();

            var result = tags.Select(x => new GetTagDto
            {
                Name = x.Name,
                Alias = x.Alias,
                Total = _posts.GetCountByTagAsync(x.Id).Result
            }).Where(x => x.Total > 0).ToList();

            response.Result = result;
            return response;
        });
    }
}