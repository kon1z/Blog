using Meowv.Blog.Application.Dto;
using Volo.Abp.Application.Services;

namespace Meowv.Blog.Application.IServices;

public interface IAuthorizeAppService : IApplicationService
{
    Task<BlogResponse<string>> GenerateTokenByCodeAsync(string code);

    Task<BlogResponse<string>> GenerateTokenAsync(AccountInput input);

    Task<BlogResponse> SendAuthorizeCodeAsync();
}