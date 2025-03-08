using Meowv.Blog.Application.Dto;

namespace Meowv.Blog.Application.IServices
{
    public partial interface ISayingService
    {
        Task<BlogResponse<string>> GetRandomAsync();
    }
}