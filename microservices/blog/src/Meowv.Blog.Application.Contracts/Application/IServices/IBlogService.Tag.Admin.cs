using Meowv.Blog.Application.Dto;
using Meowv.Blog.Application.Dto.Blog;
using Meowv.Blog.Application.Dto.Blog.Params;

namespace Meowv.Blog.Application.IServices
{
    public partial interface IBlogService
    {
        Task<BlogResponse> CreateTagAsync(CreateTagInput input);

        Task<BlogResponse> DeleteTagAsync(string id);

        Task<BlogResponse> UpdateTagAsync(string id, UpdateTagInput input);

        Task<BlogResponse<List<GetAdminTagDto>>> GetAdminTagsAsync();
    }
}