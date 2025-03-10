using Meowv.Blog;
using Meowv.Blog.MongoDb;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;

namespace Meowv.DbMigrator;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(MeowvBlogApplicationModule),
    typeof(MeowvBlogMongoDbModule)
)]
public class MeowvDbMigratorModule : AbpModule
{
}