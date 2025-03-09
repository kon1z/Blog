using System;
using System.Net.Http;
using System.Threading.Tasks;
using Meowv.Blog.Application.Dto;
using Meowv.Blog.Application.IServices;
using Meowv.Blog.Domain.Users;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Volo.Abp.DependencyInjection;

namespace Meowv.Blog.Application.OAuth.Services;

public abstract class IoAuthAppServiceBase<TOptions, TAccessToke, TUserInfo> : IOAuthAppService<TAccessToke, TUserInfo>,
    ITransientDependency where TOptions : class where TAccessToke : class where TUserInfo : class
{
    protected readonly object ServiceProviderLock = new();

    private IHttpClientFactory _httpClient;

    private IUserAppService _userAppService;

    public IServiceProvider ServiceProvider { get; set; }

    public IOptions<TOptions> Options { get; set; }

    protected IUserAppService UserAppService => LazyGetRequiredService(ref _userAppService);

    protected IHttpClientFactory HttpClient => LazyGetRequiredService(ref _httpClient);

    protected TService LazyGetRequiredService<TService>(ref TService reference)
    {
        return LazyGetRequiredService(typeof(TService), ref reference);
    }

    protected TRef LazyGetRequiredService<TRef>(Type serviceType, ref TRef reference)
    {
        if (reference == null)
            lock (ServiceProviderLock)
            {
                if (reference == null) reference = (TRef)ServiceProvider.GetRequiredService(serviceType);
            }

        return reference;
    }

    public virtual Task<string> GetAuthorizeUrl(string state)
    {
        throw new NotImplementedException();
    }

    public virtual Task<UserDto> GetUserByOAuthAsync(string type, string code, string state)
    {
        throw new NotImplementedException();
    }

    public virtual Task<TAccessToke> GetAccessTokenAsync(string code, string state)
    {
        throw new NotImplementedException();
    }

    public virtual Task<TUserInfo> GetUserInfoAsync(TAccessToke accessToken)
    {
        throw new NotImplementedException();
    }
}