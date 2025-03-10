using Meowv.Localization;
using Volo.Abp.AspNetCore.Serilog;
using Volo.Abp.Autofac;
using Volo.Abp.Data;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Volo.Abp.Swashbuckle;
using Volo.Abp.Validation;
using Volo.Abp.Validation.Localization;
using Volo.Abp.VirtualFileSystem;

namespace Meowv;

[DependsOn(typeof(AbpAspNetCoreSerilogModule),
    typeof(AbpAutofacModule),
    typeof(AbpDataModule),
    typeof(AbpValidationModule),
    typeof(AbpSwashbuckleModule)
)]
public class MeowvSharedAspnetCoreSharedModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<MeowvSharedAspnetCoreSharedModule>();
        });

        Configure<AbpLocalizationOptions>(options =>
        {
            options.Resources
                .Add<MeowvResource>("zh-Hans")
                .AddBaseTypes(
                    typeof(AbpValidationResource)
                ).AddVirtualJson("/Localization/Meowv");

            options.DefaultResourceType = typeof(MeowvResource);
        });
    }
}