using Meowv.Blog.Application.Dto;
using Meowv.Blog.Application.Dto.Blog;
using Meowv.Blog.Application.Dto.Blog.Params;

namespace Meowv.Blog.Application.IServices
{
    public partial interface IBlogService
    {
        Task<BlogResponse> CreateCategoryAsync(CreateCategoryInput input);

        Task<BlogResponse> DeleteCategoryAsync(string id);

        Task<BlogResponse> UpdateCategoryAsync(string id, UpdateCategoryInput input);

        Task<BlogResponse<List<GetAdminCategoryDto>>> GetAdminCategoriesAsync();
    }
}