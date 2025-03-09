using Volo.Abp.Http.Client;
using Volo.Abp.Modularity;

namespace Meowv.Blog;

[DependsOn(typeof(AbpHttpClientModule),
    typeof(MeowvBlogApplicationContractsModule))]
public class MeowvBlogHttpApiClientModule : AbpModule
{
}