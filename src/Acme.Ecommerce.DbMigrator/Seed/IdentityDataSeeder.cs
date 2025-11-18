using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Identity;
using Volo.Abp.Uow;
using Volo.Abp.Guids;
using Volo.Abp.PermissionManagement;
using Acme.Ecommerce.Permissions;

namespace Acme.Ecommerce.DbMigrator.Seed
{
    public class IdentityDataSeeder : IDataSeedContributor, ITransientDependency
    {
        private readonly IdentityRoleManager _roleManager;
        private readonly IdentityUserManager _userManager;
        private readonly IGuidGenerator _guidGenerator;
        private readonly IPermissionDataSeeder _permissionDataSeeder;

        public IdentityDataSeeder(
            IdentityRoleManager roleManager,
            IdentityUserManager userManager,
            IGuidGenerator guidGenerator,
            IPermissionDataSeeder permissionDataSeeder)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _guidGenerator = guidGenerator;
            _permissionDataSeeder = permissionDataSeeder;
        }

        [UnitOfWork]
        public async Task SeedAsync(DataSeedContext context)
        {
            await SeedIdentityAsync();
        }

        private async Task SeedIdentityAsync()
        {
            // ================ ROLE =================
            var adminRole = await _roleManager.FindByNameAsync("Admin");
            if (adminRole == null)
            {
                adminRole = new IdentityRole(_guidGenerator.Create(), "Admin");
                var roleResult = await _roleManager.CreateAsync(adminRole);

                if (!roleResult.Succeeded)
                    throw new System.Exception("Failed to create role Admin: " + string.Join(", ", roleResult.Errors));
            }

            // ================ USER =================
            var adminUser = await _userManager.FindByNameAsync("admin");

            if (adminUser == null)
            {
                adminUser = new IdentityUser(
                    _guidGenerator.Create(),
                    "admin",
                    "admin@email.com"
                );

                adminUser.SetEmailConfirmed(true);
                adminUser.SetPhoneNumberConfirmed(true);
                adminUser.SetIsActive(true);

                var createResult = await _userManager.CreateAsync(adminUser, "Admin@123");

                if (!createResult.Succeeded)
                    throw new System.Exception("Failed to create admin user: " + string.Join(", ", createResult.Errors));
            }
          else
{
    // First, check if the password is correct
    var isPasswordCorrect = await _userManager.CheckPasswordAsync(adminUser, "Admin@123");

    // Only update the password if it's incorrect
    if (!isPasswordCorrect)
    {
        // Remove the old password before setting a new one
        await _userManager.RemovePasswordAsync(adminUser);
        var addPasswordResult = await _userManager.AddPasswordAsync(adminUser, "Admin@123");
        
        if (!addPasswordResult.Succeeded)
        {
            throw new System.Exception("Failed to set admin password: " + string.Join(", ", addPasswordResult.Errors));
        }
    }
    
    // Ensure the user is active and confirmed
    adminUser.SetIsActive(true);
    adminUser.SetEmailConfirmed(true);
    await _userManager.UpdateAsync(adminUser);
}


            // ================ ROLE ASSIGNMENT =================
            if (!await _userManager.IsInRoleAsync(adminUser, "Admin"))
            {
                var addRoleResult = await _userManager.AddToRoleAsync(adminUser, "Admin");
                if (!addRoleResult.Succeeded)
                    throw new System.Exception("Failed to assign Admin role: " + string.Join(", ", addRoleResult.Errors));
            }

            // ================ PERMISSIONS =================
            await _permissionDataSeeder.SeedAsync(
                "R",
                "Admin",
                new[]
                {
                    EcommercePermissions.Categories.Default,
                    EcommercePermissions.Categories.Create,
                    EcommercePermissions.Categories.Edit,
                    EcommercePermissions.Categories.Delete,

                    EcommercePermissions.Products.Default,
                    EcommercePermissions.Products.Create,
                    EcommercePermissions.Products.Edit,
                    EcommercePermissions.Products.Delete
                },
                null
            );
        }
    }
}
