using System.Collections.Generic;
using System.Threading.Tasks;
using Meowv.Blog.Application.Dto;
using Meowv.Blog.Application.IServices;
using Meowv.Blog.Caching;
using Meowv.Blog.Domain.Hots;
using Meowv.Blog.Domain.Hots.Repositories;
using Meowv.Blog.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace Meowv.Blog.Application.Hots.Services;

public class HotAppService : ServiceBase, IHotAppService
{
    private readonly IHotCacheAppService _cacheApp;
    private readonly IHotRepository _hots;

    public HotAppService(IHotRepository hots, IHotCacheAppService cacheApp)
    {
        _hots = hots;
        _cacheApp = cacheApp;
    }

    /// <summary>
    ///     Get the list of sources.
    /// </summary>
    /// <returns></returns>
    [Route("api/meowv/hots/source")]
    public async Task<BlogResponse<List<HotSourceDto>>> GetSourcesAsync()
    {
        return await _cacheApp.GetSourcesAsync(async () =>
        {
            var response = new BlogResponse<List<HotSourceDto>>();

            var hots = await _hots.GetSourcesAsync();
            var result = ObjectMapper.Map<List<Hot>, List<HotSourceDto>>(hots);

            response.Result = result;
            return response;
        });
    }

    /// <summary>
    ///     Get the list of hot news by id.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [Route("api/meowv/hots/{id}")]
    public async Task<BlogResponse<HotDto>> GetHotsAsync(string id)
    {
        return await _cacheApp.GetHotsAsync(id, async () =>
        {
            var response = new BlogResponse<HotDto>();

            var hot = await _hots.GetAsync(id.ToGuid());
            if (hot is null)
            {
                response.IsFailed("The hot id not exists.");
                return response;
            }

            var result = ObjectMapper.Map<Hot, HotDto>(hot);

            response.Result = result;
            return response;
        });
    }
}