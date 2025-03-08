using System.Collections.Generic;
using System.Threading.Tasks;
using Meowv.Blog.Domain.Signatures;
using Meowv.Blog.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Meowv.Blog.Application.Signatures.Services
{
    public partial class SignatureService
    {
        /// <summary>
        /// Delete signature by id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize]
        [Route("api/meowv/signature/{id}")]
        public async Task<BlogResponse> DeleteAsync(string id)
        {
            var response = new BlogResponse();

            var saying = await _signatures.FindAsync(id.ToObjectId());
            if (saying is null)
            {
                response.IsFailed($"The signature id not exists.");
                return response;
            }

            await _signatures.DeleteAsync(id.ToObjectId());

            return response;
        }

        /// <summary>
        /// Get the list of signatures by paging.
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        [Authorize]
        [Route("api/meowv/signatures/{page}/{limit}")]
        public async Task<BlogResponse<PagedList<SignatureDto>>> GetSignaturesAsync(int page, int limit)
        {
            var response = new BlogResponse<PagedList<SignatureDto>>();

            var result = await _signatures.GetPagedListAsync(page, limit);
            var total = result.Item1;
            var signatures = ObjectMapper.Map<List<Signature>, List<SignatureDto>>(result.Item2);

            response.Result = new PagedList<SignatureDto>(total, signatures);
            return response;
        }
    }
}