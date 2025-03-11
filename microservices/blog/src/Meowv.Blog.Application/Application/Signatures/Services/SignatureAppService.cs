using Meowv.Blog.Application.Dto;
using Meowv.Blog.Application.IServices;
using Meowv.Blog.Caching;
using Meowv.Blog.Domain.Signatures;
using Meowv.Blog.Domain.Signatures.Repositories;
using Meowv.Blog.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Meowv.Blog.Application.Signatures.Services;

public class SignatureAppService : MeowvBlogAppService, ISignatureAppService
{
    private readonly ISignatureCacheAppService _cacheApp;
    private readonly ISignatureRepository _signatures;

    public SignatureAppService(ISignatureRepository signatures,
        ISignatureCacheAppService cacheApp)
    {
        _signatures = signatures;
        _cacheApp = cacheApp;
    }

    /// <summary>
    ///     Get the list of signature types.
    /// </summary>
    /// <returns></returns>
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