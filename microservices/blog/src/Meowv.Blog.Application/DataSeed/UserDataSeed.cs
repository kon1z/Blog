using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace Meowv.Blog.DataSeed;

public class UserDataSeed : ITransientDependency
{
    public async Task SeedAsync()
    {
        // TODO 重构种子数据

        //if (await _users.GetCountAsync() > 0) return;

        //var path = Path.Combine(Directory.GetCurrentDirectory(), "users.json");

        //var users = await path.FromJsonFile<List<User>>("RECORDS");
        //if (!users.Any()) return;

        //await _users.InsertManyAsync(users);

        //Console.WriteLine($"Successfully processed {users.Count} user data.");
    }
}