using Meowv.Blog.Domain.Users;
using Meowv.Blog.Domain.Users.Repositories;
using Meowv.Blog.MongoDb;
using Volo.Abp.MongoDB;

namespace Meowv.Blog.MongoDb.Repositories;

public class UserRepository : MongoDbRepositoryBase<User>, IUserRepository
{
    public UserRepository(IMongoDbContextProvider<MeowvBlogMongoDbContext> dbContextProvider) : base(dbContextProvider)
    {
    }
}