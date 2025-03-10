using System;
using Volo.Abp.Domain.Entities;

namespace Meowv.Blog.Domain.Blog;

public class Tag : Entity<Guid>
{
    /// <summary>
    ///     名称
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    ///     别名
    /// </summary>
    public string Alias { get; set; }
}