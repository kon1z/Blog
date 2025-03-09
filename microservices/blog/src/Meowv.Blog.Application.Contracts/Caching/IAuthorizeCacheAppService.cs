namespace Meowv.Blog.Caching;

public interface IAuthorizeCacheAppService
{
    Task AddAuthorizeCodeAsync(string code);

    Task<string> GetAuthorizeCodeAsync();
}