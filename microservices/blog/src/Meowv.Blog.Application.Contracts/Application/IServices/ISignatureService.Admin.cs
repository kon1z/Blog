using Meowv.Blog.Application.Dto;
using Meowv.Blog.Application.Dto.Signatures;

namespace Meowv.Blog.Application.IServices
{
    public partial interface ISignatureService
    {
        Task<BlogResponse> DeleteAsync(string id);

        Task<BlogResponse<PagedList<SignatureDto>>> GetSignaturesAsync(int page, int limit);
    }
}