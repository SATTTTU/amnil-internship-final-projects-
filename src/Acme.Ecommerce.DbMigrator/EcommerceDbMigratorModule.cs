using System;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;
using Volo.Abp.Identity;
using Volo.Abp.Account;
using Volo.Abp.PermissionManagement;
using Volo.Abp.Timing;
using Acme.Ecommerce.EntityFrameworkCore;
using Volo.Abp.OpenIddict.EntityFrameworkCore; // <-- 1. ADD THIS USING STATEMENT

namespace Acme.Ecommerce.DbMigrator;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(EcommerceEntityFrameworkCoreModule),
    typeof(EcommerceApplicationModule),
    typeof(AbpOpenIddictEntityFrameworkCoreModule), // <-- 2. ADD THIS MODULE DEPENDENCY

    typeof(AbpIdentityApplicationModule),
    typeof(AbpAccountApplicationModule),
    typeof(AbpPermissionManagementApplicationModule)
)]
public class EcommerceDbMigratorModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpClockOptions>(options =>
        {
            options.Kind = DateTimeKind.Utc;
        });
    }
}