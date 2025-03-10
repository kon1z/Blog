using System;
using System.Threading.Tasks;
using Meowv.Helpers;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace Meowv.Blog.HttpApi.Host;

public class Program
{
    public static async Task<int> Main(string[] args)
    {
        var assemblyName = typeof(Program).Assembly.GetName().Name!;
        SerilogConfigurationHelper.Configure(assemblyName);

        try
        {
            Log.Information($"Starting {assemblyName}");
            var app = await ApplicationBuilderHelper.BuildApplicationAsync<MeowvBlogHttpApiHostModule>(args);
            await app.InitializeApplicationAsync();
            await app.RunAsync();

            return 0;
        }
        catch (Exception ex)
        {
            if (ex is HostAbortedException) throw;

            Log.Fatal(ex, $"{assemblyName} terminated unexpectedly!");
            return 1;
        }
        finally
        {
            await Log.CloseAndFlushAsync();
        }
    }
}