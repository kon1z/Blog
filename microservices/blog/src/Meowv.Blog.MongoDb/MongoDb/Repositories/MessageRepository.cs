using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Meowv.Blog.Domain.Messages;
using Meowv.Blog.Domain.Messages.Repositories;
using MongoDB.Bson;
using MongoDB.Driver;
using Volo.Abp.MongoDB;

namespace Meowv.Blog.MongoDb.Repositories;

public class MessageRepository : MongoDbRepositoryBase<Message>, IMessageRepository
{
    public MessageRepository(IMongoDbContextProvider<MeowvBlogMongoDbContext> dbContextProvider) : base(
        dbContextProvider)
    {
    }

    public async Task<Tuple<int, List<Message>>> GetPagedListAsync(int skipCount, int maxResultCount)
    {
        var filter = new BsonDocument();
        var sort = new BsonDocument { { "createdAt", -1 } };

        var total = await Collection.CountDocumentsAsync(filter);
        var list = await Collection.Find(filter)
            .Sort(sort)
            .Skip((skipCount - 1) * maxResultCount)
            .Limit(maxResultCount)
            .ToListAsync();
        return new Tuple<int, List<Message>>((int)total, list);
    }
}