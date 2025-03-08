using Meowv.Blog.Application.Dto;
using Meowv.Blog.Application.Dto.Sayings;
using Meowv.Blog.Application.Dto.Sayings.Params;

namespace Meowv.Blog.Application.IServices
{
    public partial interface ISayingService
    {
        Task<BlogResponse> CreateAsync(CreateInput input);

        Task<BlogResponse> DeleteAsync(string id);

        Task<BlogResponse<PagedList<SayingDto>>> GetSayingsAsync(int page, int limit);
    }
}