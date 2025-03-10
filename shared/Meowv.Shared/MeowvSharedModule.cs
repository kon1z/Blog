using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Volo.Abp.Validation;
using Volo.Abp.Validation.Localization;
using Volo.Abp.VirtualFileSystem;

namespace Meowv
{
    [DependsOn(typeof(AbpValidationModule))]
    public class MeowvSharedModule : AbpModule
    {
     
    }
}
