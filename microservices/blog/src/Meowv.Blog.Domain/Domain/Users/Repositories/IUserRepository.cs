﻿using System;
using Volo.Abp.Domain.Repositories;

namespace Meowv.Blog.Domain.Users.Repositories;

public interface IUserRepository : IRepository<User, Guid>
{
}