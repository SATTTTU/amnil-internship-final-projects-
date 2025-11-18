using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Modularity;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.PostgreSql;

// ABP Modules required for authentication/authorization
using Volo.Abp.Identity.EntityFrameworkCore;         // Users, Roles
using Volo.Abp.PermissionManagement.EntityFrameworkCore;
using Volo.Abp.TenantManagement.EntityFrameworkCore;

// Audit Logging
using Volo.Abp.AuditLogging.EntityFrameworkCore;

using Acme.Ecommerce.Domain;

namespace Acme.Ecommerce.EntityFrameworkCore
{
    [DependsOn(
        // Your Domain Module
        typeof(EcommerceDomainModule),

        // EF Core + PostgreSQL
        typeof(AbpEntityFrameworkCorePostgreSqlModule),

        // Identity (users, roles, login)
        typeof(AbpIdentityEntityFrameworkCoreModule),

        // Permissions
        typeof(AbpPermissionManagementEntityFrameworkCoreModule),

        // Tenant Management (optional)
        typeof(AbpTenantManagementEntityFrameworkCoreModule),

        // ABP Audit Logging
        typeof(AbpAuditLoggingEntityFrameworkCoreModule)
    )]
    public class EcommerceEntityFrameworkCoreModule : AbpModule
    {
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            EcommerceEfCoreEntityExtensionMappings.Configure();
        }

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            var configuration = context.Services.GetConfiguration();

            // Register DbContext + default repositories
            context.Services.AddAbpDbContext<EcommerceDbContext>(options =>
            {
                options.AddDefaultRepositories(includeAllEntities: true);
            });

            // Configure EF Core provider + connection string
            Configure<AbpDbContextOptions>(options =>
            {
                options.Configure(config =>
                {
                    config.DbContextOptions.UseNpgsql(configuration["ConnectionStrings:Default"]);
                });
            });
        }
    }
}
