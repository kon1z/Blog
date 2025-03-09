using Meowv.Blog.Application.Dto;

namespace Meowv.Blog.Application.IServices;

public interface ISignatureAppService
{
    Task<BlogResponse<List<SignatureTypeDto>>> GetTypesAsync();

    Task<BlogResponse<string>> GenerateAsync(GenerateSignatureInput input);
    Task<BlogResponse> DeleteAsync(string id);

    Task<BlogResponse<PagedList<SignatureDto>>> GetSignaturesAsync(int page, int limit);
}