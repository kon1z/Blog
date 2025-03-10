using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Modularity;

namespace Meowv.Helpers;

public static class SwaggerConfigurationHelper
{
    public static void Configure(
        ServiceConfigurationContext context
    )
    {
        context.Services.AddAbpSwaggerGen();
    }
}