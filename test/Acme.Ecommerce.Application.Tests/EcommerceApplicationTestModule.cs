using Volo.Abp.Modularity;

namespace Acme.Ecommerce;

[DependsOn(
    typeof(EcommerceApplicationModule),
    typeof(EcommerceDomainTestModule)
)]
public class EcommerceApplicationTestModule : AbpModule
{

}
