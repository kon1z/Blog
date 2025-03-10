using Volo.Abp.Domain;
using Volo.Abp.Modularity;

namespace Meowv.Blog;

[DependsOn(typeof(AbpDddDomainModule),
    typeof(MeowvBlogDomainSharedModule))]
public class MeowvBlogDomainModule : AbpModule
{
}