namespace Meowv.Blog.Application.Dto
{
    public interface IPagedList<T> : IListResult<T>, IHasTotalCount
    {
    }
}