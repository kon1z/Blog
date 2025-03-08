using System.Threading.Tasks;
using Meowv.Blog.Domain.Sayings.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Meowv.Blog.Application.Sayings.Services
{
    public partial class SayingService : ServiceBase, ISayingService
    {
        private readonly ISayingRepository _sayings;

        public SayingService(ISayingRepository sayings)
        {
            _sayings = sayings;
        }

        /// <summary>
        /// Get a saying.
        /// </summary>
        /// <returns></returns>
        [Route("api/meowv/saying/random")]
        public async Task<BlogResponse<string>> GetRandomAsync()
        {
            var response = new BlogResponse<string>();

            var saying = await _sayings.GetRandomAsync();

            response.Result = saying.Content;
            return response;
        }
    }
}