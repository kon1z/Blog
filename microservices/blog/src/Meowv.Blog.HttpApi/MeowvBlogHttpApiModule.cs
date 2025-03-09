using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Modularity;

namespace Meowv.Blog;

[DependsOn(typeof(AbpAspNetCoreMvcModule),
    typeof(MeowvBlogApplicationContractsModule))]
public class MeowvBlogHttpApiModule : AbpModule
{
}