using Meowv.Blog.Application.Dto;

namespace Meowv.Blog.Application.IServices;

public interface IOAuthAppService<TAccessToke, TUserInfo>
{
    Task<string> GetAuthorizeUrl(string state);

    Task<UserDto> GetUserByOAuthAsync(string type, string code, string state);

    Task<TAccessToke> GetAccessTokenAsync(string code, string state);

    Task<TUserInfo> GetUserInfoAsync(TAccessToke accessToken);
}