using System.Collections.Generic;
using System.Threading.Tasks;
using Meowv.Blog.Domain.Blog;
using Microsoft.AspNetCore.Mvc;

namespace Meowv.Blog.Application.Blog.Services
{
    public partial class BlogService
    {
        /// <summary>
        /// Get the list of friendlinks.
        /// </summary>
        /// <returns></returns>
        [Route("api/meowv/blog/friendlinks")]
        public async Task<BlogResponse<List<FriendLinkDto>>> GetFriendLinksAsync()
        {
            return await _cache.GetFriendLinksAsync(async () =>
            {
                var response = new BlogResponse<List<FriendLinkDto>>();

                var friendLinks = await _friendLinks.GetListAsync();

                var result = ObjectMapper.Map<List<FriendLink>, List<FriendLinkDto>>(friendLinks);

                response.Result = result;
                return response;
            });
        }
    }
}