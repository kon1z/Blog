using Meowv.Blog.Domain.Blog;
using Meowv.Blog.Domain.Blog.Repositories;
using Meowv.Blog.MongoDb;
using Volo.Abp.MongoDB;

namespace Meowv.Blog.MongoDb.Repositories;

public class FriendLinkRepository : MongoDbRepositoryBase<FriendLink>, IFriendLinkRepository
{
    public FriendLinkRepository(IMongoDbContextProvider<MeowvBlogMongoDbContext> dbContextProvider) : base(
        dbContextProvider)
    {
    }
}