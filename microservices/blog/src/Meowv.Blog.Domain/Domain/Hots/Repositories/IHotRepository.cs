using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Bson;
using Volo.Abp.Domain.Repositories;

namespace Meowv.Blog.Domain.Hots.Repositories;

public interface IHotRepository : IRepository<Hot, ObjectId>
{
    /// <summary>
    ///     Get the list of sources.
    /// </summary>
    /// <returns></returns>
    Task<List<Hot>> GetSourcesAsync();
}