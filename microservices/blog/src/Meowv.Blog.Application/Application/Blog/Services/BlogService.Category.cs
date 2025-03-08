using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Meowv.Blog.Application.Blog.Services
{
    public partial class BlogService
    {
        /// <summary>
        /// Get the list of categories.
        /// </summary>
        /// <returns></returns>
        [Route("api/meowv/blog/categories")]
        public async Task<BlogResponse<List<GetCategoryDto>>> GetCategoriesAsync()
        {
            return await _cache.GetCategoriesAsync(async () =>
            {
                var response = new BlogResponse<List<GetCategoryDto>>();

                var categories = await _categories.GetListAsync();

                var result = categories.Select(x => new GetCategoryDto
                {
                    Name = x.Name,
                    Alias = x.Alias,
                    Total = _posts.GetCountByCategoryAsync(x.Id).Result
                }).Where(x => x.Total > 0).ToList();

                response.Result = result;
                return response;
            });
        }
    }
}