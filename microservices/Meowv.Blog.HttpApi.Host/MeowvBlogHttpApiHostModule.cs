using Autofac.Features.ResolveAnything;
using Meowv.Blog.MongoDb;
using Meowv.Helpers;
using Microsoft.AspNetCore.Cors;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Modularity;

namespace Meowv.Blog.HttpApi.Host;

[DependsOn(
    typeof(MeowvBlogApplicationModule),
    typeof(MeowvBlogMongoDbModule),
    typeof(MeowvBlogHttpApiModule),
    typeof(MeowvSharedMicroservices)
)]
public class MeowvBlogHttpApiHostModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        var configuration = context.Services.GetConfiguration();

        Configure<AbpAspNetCoreMvcOptions>(options =>
        {
            options.ConventionalControllers.Create(typeof(MeowvBlogApplicationModule).Assembly);
        });

        context.Services.AddCors(options =>
        {
            options.AddDefaultPolicy(builder =>
            {
                builder
                    .WithOrigins(configuration["App:CorsOrigins"]?
                        .Split(",", StringSplitOptions.RemoveEmptyEntries)
                        .Select(o => o.RemovePostFix("/"))
                        .ToArray() ?? [])
                    .WithAbpExposedHeaders()
                    .SetIsOriginAllowedToAllowWildcardSubdomains()
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials();
            });
        });

        context.Services.AddEndpointsApiExplorer();

        JwtBearerConfigurationHelper.Configure(context, "Meowv_BlogServer");
        SwaggerConfigurationHelper.Configure(context);
    }

    public override void OnApplicationInitialization(ApplicationInitializationContext context)
    {
        var app = context.GetApplicationBuilder();
        var env = context.GetEnvironment();

        if (env.IsDevelopment()) 
        {
            app.UseDeveloperExceptionPage();
        }
        app.UseAbpRequestLocalization();
        app.UseCorrelationId();
        app.MapAbpStaticAssets();
        app.UseRouting();
        app.UseCors();
        app.UseAuthentication();

        app.UseSwagger();
        app.UseAbpSwaggerUI();

        app.UseUnitOfWork();
        app.UseDynamicClaims();
        app.UseAuthorization();

        app.UseAuditing();
        app.UseAbpSerilogEnrichers();
        app.UseConfiguredEndpoints();
    }
}