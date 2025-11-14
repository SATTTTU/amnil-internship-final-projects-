using Microsoft.EntityFrameworkCore; 
using Volo.Abp.AuditLogging.EntityFrameworkCore;// <-- Make sure this is included!
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.PostgreSql;
using Volo.Abp.Modularity;
using Acme.Ecommerce.Domain; 

namespace Acme.Ecommerce.EntityFrameworkCore
{
    [DependsOn(typeof(EcommerceDomainModule), typeof(AbpEntityFrameworkCorePostgreSqlModule),  typeof(AbpAuditLoggingEntityFrameworkCoreModule))]
    // Add EF Core modules for ABP management features so their repositories/mappings are registered
    public class EcommerceEntityFrameworkCoreModule : AbpModule
    {
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            EcommerceEfCoreEntityExtensionMappings.Configure();
        }

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            var configuration = context.Services.GetConfiguration();

            context.Services.AddAbpDbContext<EcommerceDbContext>(options =>
            {
                /* Create default repositories for all entities */
                options.AddDefaultRepositories(includeAllEntities: true);
            });

            // Configure the EF Core DbContextOptions for ABP
            Configure<Volo.Abp.EntityFrameworkCore.AbpDbContextOptions>(abpOptions =>
            {
                abpOptions.Configure(dbContextConfigurationContext =>
                {
                    dbContextConfigurationContext.DbContextOptions.UseNpgsql(
                        configuration["ConnectionStrings:Default"]
                    );
                });
            });
        }
    }
}