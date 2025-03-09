using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace Meowv.Blog.DataSeed;

public class SayingDataSeed : ITransientDependency
{
    public Task SeedAsync()
    {
        // 重构种子数据
        return Task.CompletedTask;
        //if (await _sayings.GetCountAsync() > 0) return;

        //var path = Path.Combine(Directory.GetCurrentDirectory(), "sayings.json");

        //var sayings = await path.FromJsonFile<List<string>>("RECORDS");
        //if (!sayings.Any()) return;

        //await _sayings.InsertManyAsync(sayings.Select(item => new Saying { Content = item }));

        //Console.WriteLine($"Successfully processed {sayings.Count} saying data.");
    }
}