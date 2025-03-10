﻿using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using Volo.Abp.Modularity;

namespace Meowv.Helpers;

public static class ApplicationBuilderHelper
{
    public static async Task<WebApplication> BuildApplicationAsync<TStartupModule>(string[] args)
        where TStartupModule : IAbpModule
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.Host
            .AddAppSettingsSecretsJson()
            .UseAgileConfig()
            .UseAutofac()
            .UseSerilog();

        await builder.AddApplicationAsync<TStartupModule>();
        return builder.Build();
    }
}