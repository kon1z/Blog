using System;
using Volo.Abp.Domain.Entities;

namespace Meowv.Blog.Domain.Sayings;

public class Saying : Entity<Guid>
{
    public string Content { get; set; }
}