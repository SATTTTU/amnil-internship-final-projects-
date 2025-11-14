using Microsoft.Extensions.DependencyInjection;
// Account/Identity/TenantManagement HTTP client modules removed to keep domain-only
using Volo.Abp.Modularity;
using Volo.Abp.VirtualFileSystem;
using Volo.Abp.VirtualFileSystem;

namespace Acme.Ecommerce;

[DependsOn(
    typeof(EcommerceApplicationContractsModule)
)]
public class EcommerceHttpApiClientModule : AbpModule
{
    public const string RemoteServiceName = "Default";

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddHttpClientProxies(
            typeof(EcommerceApplicationContractsModule).Assembly,
            RemoteServiceName
        );

        Configure<AbpVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<EcommerceHttpApiClientModule>();
        });
    }
}
