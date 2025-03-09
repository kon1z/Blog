using Meowv.Blog.Application.Dto;

namespace Meowv.Blog.Application.IServices;

public interface IHotAppService
{
    Task<BlogResponse<List<HotSourceDto>>> GetSourcesAsync();

    Task<BlogResponse<HotDto>> GetHotsAsync(string id);
}