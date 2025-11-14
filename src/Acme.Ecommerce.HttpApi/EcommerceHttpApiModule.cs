using Localization.Resources.AbpUi;
using Acme.Ecommerce.Localization;
// Identity/Account/TenantManagement removed to keep app domain-only
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
// Permission/Setting management removed
using Volo.Abp.TenantManagement;

namespace Acme.Ecommerce;

[DependsOn(
    typeof(EcommerceApplicationContractsModule)
)] 
public class EcommerceHttpApiModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        ConfigureLocalization();
    }

    private void ConfigureLocalization()
    {
        Configure<AbpLocalizationOptions>(options =>
        {
            options.Resources
                .Get<EcommerceResource>()
                .AddBaseTypes(
                    typeof(AbpUiResource)
                );
        });
    }
}
