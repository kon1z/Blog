﻿using Meowv.Blog.Application.Dto;
using Volo.Abp.Application.Services;

namespace Meowv.Blog.Application.IServices;

public interface ISayingAppService : IApplicationService
{
    Task<BlogResponse<string>> GetRandomAsync();
    Task<BlogResponse> CreateAsync(CreateInput input);

    Task<BlogResponse> DeleteAsync(string id);

    Task<BlogResponse<PagedList<SayingDto>>> GetSayingsAsync(int page, int limit);
}