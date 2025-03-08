using Meowv.Blog.Application.Dto;
using Meowv.Blog.Application.Dto.Signatures;
using Meowv.Blog.Application.Dto.Signatures.Params;

namespace Meowv.Blog.Application.IServices
{
    public interface ISignatureCacheService : ICacheRemoveService
    {
        /// <summary>
        /// Get the list of signature types from the cache.
        /// </summary>
        /// <param name="func"></param>
        /// <returns></returns>
        Task<BlogResponse<List<SignatureTypeDto>>> GetTypesAsync(Func<Task<BlogResponse<List<SignatureTypeDto>>>> func);

        /// <summary>
        /// Generate a signature from the cache.
        /// </summary>
        /// <param name="input"></param>
        /// <param name="func"></param>
        /// <returns></returns>
        Task<BlogResponse<string>> GenerateAsync(GenerateSignatureInput input, Func<Task<BlogResponse<string>>> func);
    }
}