﻿using System;
using Volo.Abp.Domain.Repositories;

namespace Meowv.Blog.Domain.Blog.Repositories;

public interface IFriendLinkRepository : IRepository<FriendLink, Guid>
{
}