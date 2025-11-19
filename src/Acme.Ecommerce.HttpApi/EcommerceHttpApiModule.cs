using Localization.Resources.AbpUi;
using Acme.Ecommerce.Localization;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;

using Volo.Abp.Account;
using Volo.Abp.Identity;
using Volo.Abp.Modularity;


namespace Acme.Ecommerce;

[DependsOn(
    typeof(EcommerceApplicationContractsModule),

   // Identity User / Role controllers
   typeof(AbpIdentityHttpApiModule),
    typeof(AbpAccountHttpApiModule),
    typeof(EcommerceApplicationContractsModule)

    // Tenant Management
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
                .AddBaseTypes(typeof(AbpUiResource));
        });
    }
}
