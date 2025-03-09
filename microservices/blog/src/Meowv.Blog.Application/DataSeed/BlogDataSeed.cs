using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace Meowv.Blog.DataSeed;

public class BlogDataSeed : ITransientDependency
{
    public Task SeedAsync()
    {
        // TODO 重构种子数据

        return Task.CompletedTask;
    }
}