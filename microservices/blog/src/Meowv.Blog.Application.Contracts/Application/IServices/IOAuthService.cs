namespace Meowv.Blog.Application.IServices
{
    public interface IOAuthService<TAccessToke, TUserInfo>
    {
        Task<string> GetAuthorizeUrl(string state);

        Task<User> GetUserByOAuthAsync(string type, string code, string state);

        Task<TAccessToke> GetAccessTokenAsync(string code, string state);

        Task<TUserInfo> GetUserInfoAsync(TAccessToke accessToken);
    }
}