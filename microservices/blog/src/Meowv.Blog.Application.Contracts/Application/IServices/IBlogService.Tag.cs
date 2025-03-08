using Meowv.Blog.Application.Dto;
using Meowv.Blog.Application.Dto.Blog;

namespace Meowv.Blog.Application.IServices
{
    public partial interface IBlogService
    {
        Task<BlogResponse<List<GetTagDto>>> GetTagsAsync();
    }
}