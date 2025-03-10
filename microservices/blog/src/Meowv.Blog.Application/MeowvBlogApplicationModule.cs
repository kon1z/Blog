using Volo.Abp.Application;
using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;

namespace Meowv.Blog;

[DependsOn(
    typeof(AbpDddApplicationModule),
    typeof(AbpAutoMapperModule),
    typeof(MeowvBlogDomainModule),
    typeof(MeowvBlogApplicationContractsModule)
)]
public class MeowvBlogApplicationModule : AbpModule
{
}