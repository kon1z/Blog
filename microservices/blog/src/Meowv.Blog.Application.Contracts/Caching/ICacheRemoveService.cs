﻿namespace Meowv.Blog.Caching;

public interface ICacheRemoveService
{
    Task RemoveAsync(string key);
}