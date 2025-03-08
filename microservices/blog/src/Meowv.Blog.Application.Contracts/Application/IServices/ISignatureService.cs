using Meowv.Blog.Application.Dto;
using Meowv.Blog.Application.Dto.Signatures;
using Meowv.Blog.Application.Dto.Signatures.Params;

namespace Meowv.Blog.Application.IServices
{
    public partial interface ISignatureService
    {
        Task<BlogResponse<List<SignatureTypeDto>>> GetTypesAsync();

        Task<BlogResponse<string>> GenerateAsync(GenerateSignatureInput input);
    }
}