using Meowv.Blog.Application.Dto;
using Meowv.Blog.Application.Dto.Blog;

namespace Meowv.Blog.Application.IServices
{
    public partial interface IBlogService
    {
        Task<BlogResponse<PostDetailDto>> GetPostByUrlAsync(string url);

        Task<BlogResponse<PagedList<GetPostDto>>> GetPostsAsync(int page, int limit);

        Task<BlogResponse<List<GetPostDto>>> GetPostsByCategoryAsync(string category);

        Task<BlogResponse<List<GetPostDto>>> GetPostsByTagAsync(string tag);
    }
}