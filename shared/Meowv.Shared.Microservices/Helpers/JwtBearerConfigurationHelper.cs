using System;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Volo.Abp.Modularity;

namespace Meowv.Helpers;

public static class JwtBearerConfigurationHelper
{
    public static void Configure(
        ServiceConfigurationContext context,
        string audience)
    {
        var configuration = context.Services.GetConfiguration();

        context.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateIssuerSigningKey = true,
                    ValidateLifetime = true,
                    RequireExpirationTime = true,
                    ValidIssuer = configuration["AuthServer:Issuer"],
                    ValidAudience = audience,
                    IssuerSigningKey = new SymmetricSecurityKey(
                        (configuration["AuthServer:SigningKey"] ?? throw new InvalidOperationException()).GetBytes())
                };
            });
    }
}