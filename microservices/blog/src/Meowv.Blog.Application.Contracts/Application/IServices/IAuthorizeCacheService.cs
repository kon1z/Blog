namespace Meowv.Blog.Application.IServices
{
    public interface IAuthorizeCacheService
    {
        Task AddAuthorizeCodeAsync(string code);

        Task<string> GetAuthorizeCodeAsync();
    }
}