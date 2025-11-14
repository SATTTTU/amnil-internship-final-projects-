using Volo.Abp.Modularity;
using Volo.Abp.ObjectExtending;

namespace Acme.Ecommerce;

[DependsOn(
    typeof(EcommerceDomainSharedModule),
    typeof(AbpObjectExtendingModule)
)]
public class EcommerceApplicationContractsModule : AbpModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        EcommerceDtoExtensions.Configure();
    }
}
