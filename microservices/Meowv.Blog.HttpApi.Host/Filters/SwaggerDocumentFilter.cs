using System.Collections.Generic;
using System.Linq;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Meowv.Blog.HttpApi.Host.Filters;

public class SwaggerDocumentFilter : IDocumentFilter
{
    public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
    {
        context.ApiDescriptions.Where(x => x.RelativePath.Contains("abp")).ToList()
            ?.ForEach(x => swaggerDoc.Paths.Remove("/" + x.RelativePath));

        var tags = new List<OpenApiTag>
        {
            new() { Name = "Authorize", Description = "<code>The authorize module.</code>" },
            new() { Name = "Blog", Description = "<code>The blog module.</code>" },
            new() { Name = "Tool", Description = "<code>The tool module.</code>" },
            new() { Name = "Hot", Description = "<code>The hots module.</code>" },
            new() { Name = "Message", Description = "<code>The message module.</code>" },
            new() { Name = "Saying", Description = "<code>The sayings module.</code>" },
            new() { Name = "Signature", Description = "<code>The signature module.</code>" },
            new() { Name = "User", Description = "<code>The user module.</code>" }
        };

        swaggerDoc.Tags = tags;
    }
}