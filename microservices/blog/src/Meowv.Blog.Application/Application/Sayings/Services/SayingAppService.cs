using Meowv.Blog.Application.Dto;
using Meowv.Blog.Application.IServices;
using Meowv.Blog.Domain.Sayings;
using Meowv.Blog.Domain.Sayings.Repositories;
using Meowv.Blog.Extensions;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Meowv.Blog.Application.Sayings.Services;

public class SayingAppService : MeowvBlogAppService, ISayingAppService
{
    private readonly ISayingRepository _sayings;

    public SayingAppService(ISayingRepository sayings)
    {
        _sayings = sayings;
    }

    /// <summary>
    ///     Create sayings.
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [Authorize]
    public async Task<BlogResponse> CreateAsync(CreateInput input)
    {
        var response = new BlogResponse();
        if (!input.Content.Any())
        {
            response.IsFailed("The content list is null.");
            return response;
        }

        await _sayings.InsertManyAsync(input.Content.Select(x => new Saying { Content = x.Trim() }));

        return response;
    }

    /// <summary>
    ///     Delete saying by id.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [Authorize]
    public async Task<BlogResponse> DeleteAsync(string id)
    {
        var response = new BlogResponse();

        var saying = await _sayings.FindAsync(id.ToGuid());
        if (saying is null)
        {
            response.IsFailed("The saying id not exists.");
            return response;
        }

        await _sayings.DeleteAsync(id.ToGuid());

        return response;
    }

    /// <summary>
    ///     Get the list of sayings by paging.
    /// </summary>
    /// <param name="page"></param>
    /// <param name="limit"></param>
    /// <returns></returns>
    [Authorize]
    public async Task<BlogResponse<PagedList<SayingDto>>> GetSayingsAsync(int page, int limit)
    {
        var response = new BlogResponse<PagedList<SayingDto>>();

        var result = await _sayings.GetPagedListAsync(page, limit);
        var total = result.Item1;
        var sayings = ObjectMapper.Map<List<Saying>, List<SayingDto>>(result.Item2);

        response.Result = new PagedList<SayingDto>(total, sayings);
        return response;
    }

    /// <summary>
    ///     Get a saying.
    /// </summary>
    /// <returns></returns>
    public async Task<BlogResponse<string>> GetRandomAsync()
    {
        var response = new BlogResponse<string>();

        var saying = await _sayings.GetRandomAsync();

        response.Result = saying.Content;
        return response;
    }
}