using System;
using Volo.Abp.Domain.Entities;

namespace Meowv.Blog.Domain.Messages;

public class MessageReply : Entity<Guid> 
{
    public MessageReply()
    {
        CreatedAt = DateTime.Now;
    }

    public string UserId { get; set; }

    public string Name { get; set; }

    public string Avatar { get; set; }

    public string Content { get; set; }

    public DateTime CreatedAt { get; set; }
}