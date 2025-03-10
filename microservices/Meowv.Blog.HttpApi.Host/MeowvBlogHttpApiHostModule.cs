using Meowv.Blog.MongoDb;
using Meowv.Helpers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Cors;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Modularity;

namespace Meowv.Blog.HttpApi.Host;

[DependsOn(
    typeof(AbpAspNetCoreMvcModule),
    typeof(MeowvBlogApplicationModule),
    typeof(MeowvBlogMongoDbModule),
    typeof(MeowvBlogHttpApiModule),
    typeof(MeowvSharedMicroservices)
)]
public class MeowvBlogHttpApiHostModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpAspNetCoreMvcOptions>(options =>
        {
            options.ConventionalControllers.Create(typeof(MeowvBlogApplicationModule).Assembly);
        });

        context.Services.AddCors(options =>
        {
            options.AddPolicy("Default", builder =>
            {
                builder
                    .WithAbpExposedHeaders()
                    .SetIsOriginAllowedToAllowWildcardSubdomains()
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials();
            });
        });

        JwtBearerConfigurationHelper.Configure(context, "Meowv_BlogServer");
        SwaggerConfigurationHelper.Configure(context);
    }

    public override void OnApplicationInitialization(ApplicationInitializationContext context)
    {
        var app = context.GetApplicationBuilder();
        var env = context.GetEnvironment();

        if (env.IsDevelopment()) app.UseDeveloperExceptionPage();

        app.UseSwagger();
        app.UseSwaggerUI();

        app.UseHsts();
        app.UseRouting();
        app.UseCors("Default");
        app.UseAuthentication();
        app.UseAuthorization();
        app.UseAuditing();
        app.UseAbpSerilogEnrichers();
        app.UseConfiguredEndpoints();
    }
}