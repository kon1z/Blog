using System;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;
using Meowv.Blog.Application.Dto;
using Meowv.Blog.Application.IServices;
using Meowv.Blog.Caching;
using Meowv.Blog.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Meowv.Blog.Application.Authorize.Services;

public class AuthorizeAppService : ServiceBase, IAuthorizeAppService
{
    private readonly IAuthorizeCacheAppService _authorizeCacheAppService;
    private readonly JwtOptions _jwtOptions;
    private readonly IUserAppService _userAppService;

    public AuthorizeAppService(
        IOptions<JwtOptions> jwtOptions,
        IAuthorizeCacheAppService authorizeCacheAppService,
        IUserAppService userAppService)
    {
        _jwtOptions = jwtOptions.Value;
        _authorizeCacheAppService = authorizeCacheAppService;
        _userAppService = userAppService;
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
                $"{new DateTimeOffset(DateTime.Now.AddMinutes(_jwtOptions.Expires)).ToUnixTimeSeconds()}"),
            new Claim(JwtRegisteredClaimNames.Nbf, $"{new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds()}")
        };

        var key = new SymmetricSecurityKey(_jwtOptions.SigningKey.GetBytes());
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var securityToken = new JwtSecurityToken(
            _jwtOptions.Issuer,
            _jwtOptions.Audience,
            claims,
            expires: DateTime.Now.AddMinutes(_jwtOptions.Expires),
            signingCredentials: creds);

        var token = new JwtSecurityTokenHandler().WriteToken(securityToken);
        return token;
    }
}