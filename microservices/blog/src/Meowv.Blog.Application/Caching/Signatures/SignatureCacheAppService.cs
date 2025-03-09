using Meowv.Blog.Application.Dto;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Meowv.Blog.Caching.Signatures;

public class SignatureCacheAppService : CachingServiceBase, ISignatureCacheAppService
{
    public async Task<BlogResponse<List<SignatureTypeDto>>> GetTypesAsync(
        Func<Task<BlogResponse<List<SignatureTypeDto>>>> func)
    {
        return await Cache.GetOrAddAsync(CachingConsts.CacheKeys.GetSignatureTypes(), func,
            CachingConsts.CacheStrategy.ONE_HOURS);
    }

    public async Task<BlogResponse<string>> GenerateAsync(GenerateSignatureInput input,
        Func<Task<BlogResponse<string>>> func)
    {
        return await Cache.GetOrAddAsync(CachingConsts.CacheKeys.GenerateSignature(input.Name, input.TypeId), func,
            CachingConsts.CacheStrategy.ONE_HOURS);
    }
}