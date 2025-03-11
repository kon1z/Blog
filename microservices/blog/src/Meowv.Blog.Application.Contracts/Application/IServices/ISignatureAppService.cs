using Meowv.Blog.Application.Dto;
using Volo.Abp.Application.Services;

namespace Meowv.Blog.Application.IServices;

public interface ISignatureAppService : IApplicationService
{
    Task<BlogResponse<List<SignatureTypeDto>>> GetTypesAsync();

    Task<BlogResponse<string>> GenerateAsync(GenerateSignatureInput input);
    Task<BlogResponse> DeleteAsync(string id);

    Task<BlogResponse<PagedList<SignatureDto>>> GetSignaturesAsync(int page, int limit);
}