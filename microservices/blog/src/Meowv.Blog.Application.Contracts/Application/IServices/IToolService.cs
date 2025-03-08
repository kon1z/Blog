using Meowv.Blog.Application.Dto;
using Meowv.Blog.Application.Dto.Tools.Params;

namespace Meowv.Blog.Application.IServices
{
    public interface IToolService
    {
        Task<BlogResponse<string>> GetBingBackgroundUrlAsync();

        Task<FileContentResult> GetBingBackgroundImgAsync();

        Task<BlogResponse<List<string>>> Ip2RegionAsync(string ip);

        Task<BlogResponse> SendMessageAsync(SendMessageInput input);

        Task<FileContentResult> GetImgAsync(string url);

        Task<BlogResponse<PurgeUrlsCacheResponse>> PurgeCdnUrlsAsync(List<string> urls);

        Task<BlogResponse<PurgePathCacheResponse>> PurgeCdnPathsAsync(List<string> paths);

        Task<BlogResponse<PushUrlsCacheResponse>> PushCdnUrlsAsync(List<string> urls);
    }
}