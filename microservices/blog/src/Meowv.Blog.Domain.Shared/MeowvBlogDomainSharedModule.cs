using Volo.Abp.Domain;
using Volo.Abp.Modularity;

namespace Meowv.Blog;

[DependsOn(typeof(AbpDddDomainSharedModule))]
public class MeowvBlogDomainSharedModule : AbpModule
{
}