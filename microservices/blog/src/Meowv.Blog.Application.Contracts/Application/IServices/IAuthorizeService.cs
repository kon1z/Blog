using Meowv.Blog.Application.Dto;
using Meowv.Blog.Application.Dto.Authorize.Params;

namespace Meowv.Blog.Application.IServices
{
    public interface IAuthorizeService
    {
        Task<BlogResponse<string>> GetAuthorizeUrlAsync(string type);

        Task<BlogResponse<string>> GenerateTokenAsync(string type, string code, string state);

        Task<BlogResponse<string>> GenerateTokenAsync(string code);

        Task<BlogResponse<string>> GenerateTokenAsync(IUserService userService, AccountInput input);

        Task<BlogResponse> SendAuthorizeCodeAsync();
    }
}