using Meowv.Blog.Application.Dto;

namespace Meowv.Blog.Application.IServices
{
    public partial interface IBlogService
    {
        Task<BlogResponse<Tuple<int, int, int>>> GetStatisticsAsync();
    }
}