using Meowv.Blog.Application.Dto;
using Meowv.Blog.Application.IServices;
using Meowv.Blog.Caching;
using Meowv.Blog.Domain.Signatures;
using Meowv.Blog.Domain.Signatures.Repositories;
using Meowv.Blog.Extensions;
using Meowv.Blog.Options;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Meowv.Blog.Application.Signatures.Services;

public class SignatureAppService : ServiceBase, ISignatureAppService
{
    private readonly ISignatureCacheAppService _cacheApp;
    private readonly IHttpClientFactory _httpClient;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly SignatureOptions _signatureOptions;
    private readonly ISignatureRepository _signatures;

    public SignatureAppService(ISignatureRepository signatures,
        IHttpClientFactory httpClient,
        IHttpContextAccessor httpContextAccessor,
        ISignatureCacheAppService cacheApp,
        IOptions<SignatureOptions> signatureOptions)
    {
        _signatures = signatures;
        _httpClient = httpClient;
        _httpContextAccessor = httpContextAccessor;
        _cacheApp = cacheApp;
        _signatureOptions = signatureOptions.Value;
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
    public async Task<BlogResponse<string>> GenerateAsync(GenerateSignatureInput input)
    {
        return await _cacheApp.GenerateAsync(input, async () =>
        {
            var response = new BlogResponse<string>();

            var ip = Enumerable.FirstOrDefault<string>(_httpContextAccessor.HttpContext.Request.Headers["X-Real-IP"]) ??
                     Enumerable.FirstOrDefault<string>(_httpContextAccessor.HttpContext.Request.Headers["X-Forwarded-For"]) ??
                     _httpContextAccessor.HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();

            var type = Signature.KnownTypes.Dictionary.FirstOrDefault(x => x.Value == input.TypeId).Key;
            if (type.IsNullOrEmpty())
            {
                response.IsFailed("The signature type not exists.");
                return response;
            }

            var signature = await _signatures.FindAsync(x => x.Name == input.Name && x.Type == type);
            if (signature is not null)
            {
                response.Result = signature.Url;
                return response;
            }

            var api = Enumerable
                .OrderBy<KeyValuePair<string, string>, Guid>(_signatureOptions.Urls, x => GuidGenerator.Create())
                .Select(x => new { Url = x.Key, Param = string.Format(x.Value, input.Name, input.TypeId) })
                .FirstOrDefault();

            var content = new StringContent(api.Param);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/x-www-form-urlencoded");
            using var client = HttpClientFactoryExtensions.CreateClient(_httpClient);
            client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0");
            var httpResponse = await client.PostAsync(api.Url, content);
            var httpResult = await httpResponse.Content.ReadAsStringAsync();

            var regex = new Regex(
                @"<img\b[^<>]*?\bsrc[\s\t\r\n]*=[\s\t\r\n]*[""']?[\s\t\r\n]*(?<imgUrl>[^\s\t\r\n""'<>]*)[^<>]*?/?[\s\t\r\n]*>",
                RegexOptions.IgnoreCase);
            var imgUrl = regex.Match(httpResult).Groups["imgUrl"].Value;

            var url = $"{$"{input.Name}_{type}".ToMd5()}.png";
            var signaturePath = Path.Combine(_signatureOptions.Path, url);

            var imgBuffer = await client.GetByteArrayAsync(imgUrl);
            await imgBuffer.DownloadAsync(signaturePath);

            await _signatures.InsertAsync(new Signature
            {
                Name = input.Name,
                Type = type,
                Url = url,
                Ip = ip
            });

            response.Result = url;
            return response;
        });
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

        var saying = await _signatures.FindAsync(id.ToObjectId());
        if (saying is null)
        {
            response.IsFailed("The signature id not exists.");
            return response;
        }

        await _signatures.DeleteAsync(id.ToObjectId());

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