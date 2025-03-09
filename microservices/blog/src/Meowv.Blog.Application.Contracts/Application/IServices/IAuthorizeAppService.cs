using Meowv.Blog.Application.Dto;

namespace Meowv.Blog.Application.IServices;

public interface IAuthorizeAppService
{
    Task<BlogResponse<string>> GetAuthorizeUrlAsync(string type);

    Task<BlogResponse<string>> GenerateTokenAsync(string type, string code, string state);

    Task<BlogResponse<string>> GenerateTokenAsync(string code);

    Task<BlogResponse<string>> GenerateTokenAsync(IUserAppService userAppService, AccountInput input);

    Task<BlogResponse> SendAuthorizeCodeAsync();
}