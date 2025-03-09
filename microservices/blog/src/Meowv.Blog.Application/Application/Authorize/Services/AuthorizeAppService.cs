using Meowv.Blog.Application.Dto;
using Meowv.Blog.Application.IServices;
using Meowv.Blog.Application.OAuth;
using Meowv.Blog.Application.OAuth.Services;
using Meowv.Blog.Caching;
using Meowv.Blog.Domain.Users;
using Meowv.Blog.Extensions;
using Meowv.Blog.Options;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Meowv.Blog.Application.Authorize.Services;

public class AuthorizeAppService : ServiceBase, IAuthorizeAppService
{
    private readonly IoAuthAppAlipayService _appAlipayService;
    private readonly IAuthorizeCacheAppService _authorizeCacheAppService;
    private readonly IoAuthAppDingtalkService _appDingtalkService;
    private readonly IoAuthAppGiteeService _appGiteeService;
    private readonly IoAuthAppGithubService _appGithubService;
    private readonly JwtOptions _jwtOption;
    private readonly IoAuthAppMicrosoftService _appMicrosoftService;
    private readonly IoAuthAppQqService _appQqService;
    private readonly IUserAppService _userAppService;
    private readonly IoAuthAppWeiboService _appWeiboService;

    public AuthorizeAppService(IOptions<JwtOptions> jwtOption,
        IAuthorizeCacheAppService authorizeCacheAppService,
        IUserAppService userAppService,
        IoAuthAppGithubService appGithubService,
        IoAuthAppGiteeService appGiteeService,
        IoAuthAppAlipayService appAlipayService,
        IoAuthAppDingtalkService appDingtalkService,
        IoAuthAppMicrosoftService appMicrosoftService,
        IoAuthAppWeiboService appWeiboService,
        IoAuthAppQqService appQqService)
    {
        _jwtOption = jwtOption.Value;
        _authorizeCacheAppService = authorizeCacheAppService;
        _userAppService = userAppService;
        _appGithubService = appGithubService;
        _appGiteeService = appGiteeService;
        _appAlipayService = appAlipayService;
        _appDingtalkService = appDingtalkService;
        _appMicrosoftService = appMicrosoftService;
        _appWeiboService = appWeiboService;
        _appQqService = appQqService;
    }

    /// <summary>
    ///     Get authorize url.
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    [Route("api/meowv/oauth/{type}")]
    public async Task<BlogResponse<string>> GetAuthorizeUrlAsync(string type)
    {
        var state = StateManager.Instance.Get();

        var response = new BlogResponse<string>
        {
            Result = type switch
            {
                "github" => await _appGithubService.GetAuthorizeUrl(state),
                "gitee" => await _appGiteeService.GetAuthorizeUrl(state),
                "alipay" => await _appAlipayService.GetAuthorizeUrl(state),
                "dingtalk" => await _appDingtalkService.GetAuthorizeUrl(state),
                "microsoft" => await _appMicrosoftService.GetAuthorizeUrl(state),
                "weibo" => await _appWeiboService.GetAuthorizeUrl(state),
                "qq" => await _appQqService.GetAuthorizeUrl(state),
                _ => throw new NotImplementedException($"Not implemented:{type}")
            }
        };

        return response;
    }

    /// <summary>
    ///     Generate token by <paramref name="type" />.
    /// </summary>
    /// <param name="type"></param>
    /// <param name="code"></param>
    /// <param name="state"></param>
    /// <returns></returns>
    [HttpGet]
    [Route("api/meowv/oauth/{type}/token")]
    public async Task<BlogResponse<string>> GenerateTokenAsync(string type, string code, string state)
    {
        var response = new BlogResponse<string>();

        if (!StateManager.IsExist(state))
        {
            response.IsFailed("Request failed.");
            return response;
        }

        StateManager.Remove(state);

        var token = type switch
        {
            "github" => GenerateToken(await _appGithubService.GetUserByOAuthAsync(type, code, state)),
            "gitee" => GenerateToken(await _appGiteeService.GetUserByOAuthAsync(type, code, state)),
            "alipay" => GenerateToken(await _appAlipayService.GetUserByOAuthAsync(type, code, state)),
            "dingtalk" => GenerateToken(await _appDingtalkService.GetUserByOAuthAsync(type, code, state)),
            "microsoft" => GenerateToken(await _appMicrosoftService.GetUserByOAuthAsync(type, code, state)),
            "weibo" => GenerateToken(await _appWeiboService.GetUserByOAuthAsync(type, code, state)),
            "qq" => GenerateToken(await _appQqService.GetUserByOAuthAsync(type, code, state)),
            _ => throw new NotImplementedException($"Not implemented:{type}")
        };

        response.IsSuccess(token);
        return response;
    }

    /// <summary>
    ///     Generate token by authorization code.
    /// </summary>
    /// <param name="code"></param>
    /// <returns></returns>
    [Route("api/meowv/oauth/token")]
    public async Task<BlogResponse<string>> GenerateTokenAsync([Required] string code)
    {
        var response = new BlogResponse<string>();

        var cacheCode = await _authorizeCacheAppService.GetAuthorizeCodeAsync();
        if (code != cacheCode)
        {
            response.IsFailed("The authorization code is wrong.");
            return response;
        }

        var user = await _userAppService.GetDefaultUserAsync();
        var token = GenerateToken(user);

        response.IsSuccess(token);
        return response;
    }

    /// <summary>
    ///     Generate token by account.
    /// </summary>
    /// <param name="userAppService"></param>
    /// <param name="input"></param>
    /// <returns></returns>
    [Route("api/meowv/oauth/account/token")]
    public async Task<BlogResponse<string>> GenerateTokenAsync([FromServices] IUserAppService userAppService,
        AccountInput input)
    {
        var response = new BlogResponse<string>();

        var user = await userAppService.VerifyByAccountAsync(input.Username, input.Password);
        var token = GenerateToken(user);

        response.IsSuccess(token);
        return await Task.FromResult(response);
    }

    /// <summary>
    ///     Send authorization code.
    /// </summary>
    /// <returns></returns>
    [Route("api/meowv/oauth/code/send")]
    public async Task<BlogResponse> SendAuthorizeCodeAsync()
    {
        var response = new BlogResponse();

        var length = 6;
        var code = length.GenerateRandomCode();

        await _authorizeCacheAppService.AddAuthorizeCodeAsync(code);

        //await _toolAppService.SendMessageAsync(new SendMessageInput
        //{
        //    Text = code
        //});

        return response;
    }

    private string GenerateToken(UserDto user)
    {
        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id),
            new Claim(ClaimTypes.Name, user.Name),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim("avatar", user.Avatar),
            new Claim(JwtRegisteredClaimNames.Exp,
                $"{new DateTimeOffset(DateTime.Now.AddMinutes(_jwtOption.Expires)).ToUnixTimeSeconds()}"),
            new Claim(JwtRegisteredClaimNames.Nbf, $"{new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds()}")
        };

        var key = new SymmetricSecurityKey(_jwtOption.SigningKey.GetBytes());
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var securityToken = new JwtSecurityToken(
            _jwtOption.Issuer,
            _jwtOption.Audience,
            claims,
            expires: DateTime.Now.AddMinutes(_jwtOption.Expires),
            signingCredentials: creds);

        var token = new JwtSecurityTokenHandler().WriteToken(securityToken);
        return token;
    }
}