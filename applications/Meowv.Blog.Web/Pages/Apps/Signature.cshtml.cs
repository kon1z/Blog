using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Meowv.Blog.Application.Dto;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Meowv.Blog.Web.Pages.Apps;

public class SignatureModel : PageBase
{
    public SignatureModel(IHttpClientFactory httpClientFactory) : base(httpClientFactory)
    {
    }

    public List<SelectListItem> Options { get; set; }

    public async Task OnGetAsync()
    {
        var signatureTypes = await GetResultAsync<BlogResponse<List<SignatureTypeDto>>>("api/meowv/signature/types");

        Options = signatureTypes.Result.Select(x => new SelectListItem
        {
            Text = x.Type,
            Value = x.TypeId.ToString()
        }).ToList();
    }
}