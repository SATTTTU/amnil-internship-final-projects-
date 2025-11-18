using Microsoft.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.Modeling; 
using Acme.Ecommerce.Domain.Entities;
using Volo.Abp.Identity.EntityFrameworkCore;
using Volo.Abp.OpenIddict.EntityFrameworkCore;
using Volo.Abp.PermissionManagement.EntityFrameworkCore;
using Volo.Abp.SettingManagement.EntityFrameworkCore;
using Volo.Abp.BackgroundJobs.EntityFrameworkCore;
using Volo.Abp.AuditLogging.EntityFrameworkCore;
using Volo.Abp.FeatureManagement.EntityFrameworkCore;
using Volo.Abp.TenantManagement.EntityFrameworkCore;


namespace Acme.Ecommerce.EntityFrameworkCore;

[ConnectionStringName("Default")]
public class EcommerceDbContext : AbpDbContext<EcommerceDbContext>
{
    public DbSet<Product> Products { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Inventory> Inventories { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }

    public EcommerceDbContext(DbContextOptions<EcommerceDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ConfigureIdentity();
        builder.ConfigureOpenIddict();
        builder.ConfigurePermissionManagement();
        builder.ConfigureSettingManagement();
        builder.ConfigureBackgroundJobs();
        builder.ConfigureAuditLogging();
        builder.ConfigureFeatureManagement();
        builder.ConfigureTenantManagement();

        builder.Entity<Product>(b =>
        {
            b.ToTable("Products");
            b.ConfigureByConvention(); 
        });

        builder.Entity<Category>(b =>
        {
            b.ToTable("Categories");
            b.ConfigureByConvention(); 
        });

        builder.Entity<Inventory>(b =>
        {
            b.ToTable("Inventories");
            b.ConfigureByConvention(); 
        });

        builder.Entity<Order>(b =>
        {
            b.ToTable("Orders");
            b.ConfigureByConvention();
        });

        builder.Entity<OrderItem>(b =>
        {
            b.ToTable("OrderItems");
            b.ConfigureByConvention(); 
        });
    }
}