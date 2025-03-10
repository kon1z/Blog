using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Meowv.Blog.Application.Dto;
using Meowv.Blog.Application.IServices;
using Meowv.Blog.Caching;
using Meowv.Blog.Domain.Signatures;
using Meowv.Blog.Domain.Signatures.Repositories;
using Meowv.Blog.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Meowv.Blog.Application.Signatures.Services;

public class SignatureAppService : ServiceBase, ISignatureAppService
{
    private readonly ISignatureCacheAppService _cacheApp;
    private readonly IHttpClientFactory _httpClient;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly ISignatureRepository _signatures;

    public SignatureAppService(ISignatureRepository signatures,
        IHttpClientFactory httpClient,
        IHttpContextAccessor httpContextAccessor,
        ISignatureCacheAppService cacheApp)
    {
        _signatures = signatures;
        _httpClient = httpClient;
        _httpContextAccessor = httpContextAccessor;
        _cacheApp = cacheApp;
    }

    /// <summary>
    ///     Get the list of signature types.
    /// </summary>
    /// <returns></returns>
    [Route("api/meowv/signature/types")]
    public async Task<BlogResponse<List<SignatureTypeDto>>> GetTypesAsync()
    {
        return await _cacheApp.GetTypesAsync(async () =>
        {
            var response = new BlogResponse<List<SignatureTypeDto>>();

            var result = Signature.KnownTypes.Dictionary.Select(x => new SignatureTypeDto
            {
                Type = x.Key,
                TypeId = x.Value
            }).ToList();

            response.Result = result;
            return await Task.FromResult(response);
        });
    }

    /// <summary>
    ///     Generate a signature.
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [Route("api/meowv/signature/generate")]
    public Task<BlogResponse<string>> GenerateAsync(GenerateSignatureInput input)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    ///     Delete signature by id.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [Authorize]
    [Route("api/meowv/signature/{id}")]
    public async Task<BlogResponse> DeleteAsync(string id)
    {
        var response = new BlogResponse();

        var saying = await _signatures.FindAsync(id.ToGuid());
        if (saying is null)
        {
            response.IsFailed("The signature id not exists.");
            return response;
        }

        await _signatures.DeleteAsync(id.ToGuid());

        return response;
    }

    /// <summary>
    ///     Get the list of signatures by paging.
    /// </summary>
    /// <param name="page"></param>
    /// <param name="limit"></param>
    /// <returns></returns>
    [Authorize]
    [Route("api/meowv/signatures/{page}/{limit}")]
    public async Task<BlogResponse<PagedList<SignatureDto>>> GetSignaturesAsync(int page, int limit)
    {
        var response = new BlogResponse<PagedList<SignatureDto>>();

        var result = await _signatures.GetPagedListAsync(page, limit);
        var total = result.Item1;
        var signatures = ObjectMapper.Map<List<Signature>, List<SignatureDto>>(result.Item2);

        response.Result = new PagedList<SignatureDto>(total, signatures);
        return response;
    }
}