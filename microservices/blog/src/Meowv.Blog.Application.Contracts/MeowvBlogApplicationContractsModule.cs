using Volo.Abp.Application;
using Volo.Abp.Modularity;

namespace Meowv.Blog;

[DependsOn(typeof(AbpDddApplicationContractsModule))]
public class MeowvBlogApplicationContractsModule : AbpModule
{
}