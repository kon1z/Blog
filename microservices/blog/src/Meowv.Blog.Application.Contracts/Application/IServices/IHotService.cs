using Meowv.Blog.Application.Dto;
using Meowv.Blog.Application.Dto.Hots;

namespace Meowv.Blog.Application.IServices
{
    public interface IHotService
    {
        Task<BlogResponse<List<HotSourceDto>>> GetSourcesAsync();

        Task<BlogResponse<HotDto>> GetHotsAsync(string id);
    }
}