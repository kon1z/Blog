using Meowv.Blog.Application.Dto;
using Meowv.Blog.Application.Dto.Blog;
using Meowv.Blog.Application.Dto.Blog.Params;

namespace Meowv.Blog.Application.IServices
{
    public partial interface IBlogService
    {
        Task<BlogResponse> CreatePostAsync(CreatePostInput input);

        Task<BlogResponse> DeletePostAsync(string id);

        Task<BlogResponse> UpdatePostAsync(string id, UpdatePostInput input);

        Task<BlogResponse<PostDto>> GetPostAsync(string id);

        Task<BlogResponse<PagedList<GetAdminPostDto>>> GetAdminPostsAsync(int page, int limit);
    }
}