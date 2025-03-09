using System.IO;
using Meowv.Blog;
using Meowv.Blog.MongoDb;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Autofac;
using Volo.Abp.Data;
using Volo.Abp.Modularity;

namespace Meowv.DbMigrator;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(MeowvBlogApplicationModule),
    typeof(MeowvBlogMongoDbModule)
)]
public class MeowvDbMigratorModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        var config = ConfigurationHelper.BuildConfiguration();

        context.Services.Configure<AbpDbConnectionOptions>(options =>
        {
            options.ConnectionStrings.Default = config.GetSection("storage").GetValue<string>("mongodb");
        });
    }
}