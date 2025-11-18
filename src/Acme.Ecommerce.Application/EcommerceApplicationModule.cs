using Volo.Abp.Application;
using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;

namespace Acme.Ecommerce;

[DependsOn(
    typeof(EcommerceDomainModule),
    typeof(EcommerceApplicationContractsModule),

    // REQUIRED ABP base modules
    typeof(AbpDddApplicationModule),
    typeof(AbpDddApplicationContractsModule),

    // Automapper
    typeof(AbpAutoMapperModule)
)]
public class EcommerceApplicationModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpAutoMapperOptions>(options =>
        {
            options.AddMaps<EcommerceApplicationModule>(validate: true);
        });
    }
}
