using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Modularity;

namespace Meowv.Blog;

[DependsOn(typeof(AbpAspNetCoreMvcModule),
    typeof(MeowvBlogApplicationContractsModule))]
public class MeowvBlogHttpApiModule : AbpModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        PreConfigure<IMvcBuilder>(mvcBuilder =>
        {
            mvcBuilder.AddApplicationPartIfNotExists(typeof(MeowvBlogHttpApiModule).Assembly);
        });
    }
}