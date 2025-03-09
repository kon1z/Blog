using System.Text;
using Meowv.Blog;
using Meowv.Blog.Options;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.BackgroundWorkers.Quartz;
using Volo.Abp.Modularity;

namespace Meowv;

[DependsOn(
    typeof(AbpBackgroundWorkersQuartzModule),
    typeof(MeowvBlogDomainModule)
)]
public class MeowvBackgroundWorkersModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        var option = context.Services.ExecutePreConfiguredActions<WorkerOptions>();

        Configure<AbpBackgroundWorkerQuartzOptions>(options => { options.IsAutoRegisterEnabled = option.IsEnabled; });

        context.Services.AddHttpClient();

        Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
    }
}