using Acme.Ecommerce.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace Acme.Ecommerce.Permissions;

public class EcommercePermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var ecommerceGroup = context.AddGroup(EcommercePermissions.GroupName, L("Permission:Ecommerce"));

        //Define your own permissions here.


        var categoriesPermission = ecommerceGroup.AddPermission(EcommercePermissions.Categories.Default, L("Permission:Categories"));
        categoriesPermission.AddChild(EcommercePermissions.Categories.Create, L("Permission:Categories.Create"));
        categoriesPermission.AddChild(EcommercePermissions.Categories.Edit, L("Permission:Categories.Edit"));
        categoriesPermission.AddChild(EcommercePermissions.Categories.Delete, L("Permission:Categories.Delete"));



        var productsPermission = ecommerceGroup.AddPermission(EcommercePermissions.Products.Default, L("Permission:Products"));
        productsPermission.AddChild(EcommercePermissions.Products.Create, L("Permission:Products.Create"));
        productsPermission.AddChild(EcommercePermissions.Products.Edit, L("Permission:Products.Edit"));
        productsPermission.AddChild(EcommercePermissions.Products.Delete, L("Permission:Products.Delete"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<EcommerceResource>(name);
    }
}