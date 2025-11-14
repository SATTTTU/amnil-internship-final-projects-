using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;

namespace Acme.Ecommerce;

[DependsOn(
    typeof(EcommerceDomainModule),
    typeof(EcommerceApplicationContractsModule),
    typeof(AbpAutoMapperModule)
)]
public class EcommerceApplicationModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpAutoMapperOptions>(options =>
        {
            // Automatically load all AutoMapper Profile classes in this assembly
            options.AddMaps<EcommerceApplicationModule>(validate: true);
        });
    }
}
