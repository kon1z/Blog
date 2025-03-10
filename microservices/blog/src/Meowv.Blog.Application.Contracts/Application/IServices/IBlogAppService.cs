using Meowv.Blog.Application.Dto;

namespace Meowv.Blog.Application.IServices;

public interface IBlogAppService
{
    Task<BlogResponse<Tuple<int, int, int>>> GetStatisticsAsync();
    Task<BlogResponse> CreateCategoryAsync(CreateCategoryInput input);

    Task<BlogResponse> DeleteCategoryAsync(string id);

    Task<BlogResponse> UpdateCategoryAsync(string id, UpdateCategoryInput input);

    Task<BlogResponse<List<GetAdminCategoryDto>>> GetAdminCategoriesAsync();
    Task<BlogResponse<List<GetCategoryDto>>> GetCategoriesAsync();

    Task<BlogResponse<List<GetTagDto>>> GetTagsAsync();
    Task<BlogResponse> CreateTagAsync(CreateTagInput input);

    Task<BlogResponse> DeleteTagAsync(string id);

    Task<BlogResponse> UpdateTagAsync(string id, UpdateTagInput input);

    Task<BlogResponse<List<GetAdminTagDto>>> GetAdminTagsAsync();

    Task<BlogResponse<PostDetailDto>> GetPostByUrlAsync(string url);

    Task<BlogResponse<PagedList<GetPostDto>>> GetPostsAsync(int page, int limit);

    Task<BlogResponse<List<GetPostDto>>> GetPostsByCategoryAsync(string category);

    Task<BlogResponse<List<GetPostDto>>> GetPostsByTagAsync(string tag);

    Task<BlogResponse> CreatePostAsync(CreatePostInput input);

    Task<BlogResponse> DeletePostAsync(string id);

    Task<BlogResponse> UpdatePostAsync(string id, UpdatePostInput input);

    Task<BlogResponse<PostDto>> GetPostAsync(string id);

    Task<BlogResponse<PagedList<GetAdminPostDto>>> GetAdminPostsAsync(int page, int limit);

    Task<BlogResponse<List<FriendLinkDto>>> GetFriendLinksAsync();
    Task<BlogResponse> CreateFriendLinkAsync(CreateFriendLinkInput input);

    Task<BlogResponse> DeleteFriendLinkAsync(string id);

    Task<BlogResponse> UpdateFriendLinkAsync(string id, UpdateFriendLinkInput input);

    Task<BlogResponse<List<GetAdminFriendLinkDto>>> GetAdminFriendLinksAsync();
}