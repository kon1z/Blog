using Meowv.Blog;
using Volo.Abp.BackgroundWorkers.Quartz;
using Volo.Abp.Modularity;

namespace Meowv;

[DependsOn(
    typeof(AbpBackgroundWorkersQuartzModule),
    typeof(MeowvBlogDomainModule)
)]
public class MeowvBackgroundWorkersModule : AbpModule
{
}