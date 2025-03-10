using System;
using System.Collections.Generic;
using Volo.Abp.Domain.Entities;

namespace Meowv.Blog.Domain.Signatures;

public class Signature : Entity<Guid>
{
    public string Name { get; set; }

    public string Type { get; set; }

    public string Url { get; set; }

    public string Ip { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.Now;

    public static class KnownTypes
    {
        public static Dictionary<string, int> Dictionary { get; set; } = new()
        {
            { "一笔艺术签", 901 },
            { "连笔商务签", 904 },
            { "一笔商务签", 905 }
        };
    }
}