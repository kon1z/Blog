using Meowv.Blog.Application.Dto;
using Volo.Abp.Application.Services;

namespace Meowv.Blog.Application.IServices;

public interface IHotAppService : IApplicationService
{
    Task<BlogResponse<List<HotSourceDto>>> GetSourcesAsync();

    Task<BlogResponse<HotDto>> GetHotsAsync(string id);
}