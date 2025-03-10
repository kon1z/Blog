using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Distributed;

namespace Meowv.Blog.Caching.Authorize;

public class AuthorizeCacheAppService : CachingServiceBase, IAuthorizeCacheAppService
{
    public async Task AddAuthorizeCodeAsync(string code)
    {
        await Cache.SetStringAsync(CachingConsts.CachePrefix.Authorize, code);
    }

    public async Task<string> GetAuthorizeCodeAsync()
    {
        return await Cache.GetStringAsync(CachingConsts.CachePrefix.Authorize);
    }
}