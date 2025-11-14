using Microsoft.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.Modeling;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;
using Acme.Ecommerce.Domain.Entities;




namespace Acme.Ecommerce.EntityFrameworkCore;

[ConnectionStringName("Default")]
public class EcommerceDbContext :
    AbpDbContext<EcommerceDbContext>
{
    /* Add DbSet properties for your Aggregate Roots / Entities here. */
    public DbSet<Product> Products { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Inventory> Inventories { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }

    #region Entities from the modules

    // Identity
    //public DbSet<IdentityUser> Users { get; set; }
    //public DbSet<IdentityRole> Roles { get; set; }
    //public DbSet<IdentityClaimType> ClaimTypes { get; set; }
    //public DbSet<OrganizationUnit> OrganizationUnits { get; set; }
    //public DbSet<IdentitySecurityLog> SecurityLogs { get; set; }
    //public DbSet<IdentityLinkUser> LinkUsers { get; set; }
    //public DbSet<IdentityUserDelegation> UserDelegations { get; set; }
    //public DbSet<IdentitySession> Sessions { get; set; }

    // Tenant Management
    //public DbSet<Tenant> Tenants { get; set; }
    //public DbSet<TenantConnectionString> TenantConnectionStrings { get; set; }

    #endregion

    public EcommerceDbContext(DbContextOptions<EcommerceDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        // Configure your own entities
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
        // (Only domain entities are configured here.)
    }
}
