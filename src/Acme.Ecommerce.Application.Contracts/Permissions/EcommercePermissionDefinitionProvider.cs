using Acme.Ecommerce.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace Acme.Ecommerce.Permissions;

public class EcommercePermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var ecommerceGroup = context.AddGroup(EcommercePermissions.GroupName, L("Permission:Ecommerce"));

        // ------------------------------------------------------
        // CATEGORIES
        // ------------------------------------------------------
        var categoriesPermission = ecommerceGroup.AddPermission(
            EcommercePermissions.Categories.Default,
            L("Permission:Categories")
        );
        categoriesPermission.AddChild(EcommercePermissions.Categories.Create, L("Permission:Categories.Create"));
        categoriesPermission.AddChild(EcommercePermissions.Categories.Edit, L("Permission:Categories.Edit"));
        categoriesPermission.AddChild(EcommercePermissions.Categories.Delete, L("Permission:Categories.Delete"));


        // ------------------------------------------------------
        // PRODUCTS
        // ------------------------------------------------------
        var productsPermission = ecommerceGroup.AddPermission(
            EcommercePermissions.Products.Default,
            L("Permission:Products")
        );
        productsPermission.AddChild(EcommercePermissions.Products.Create, L("Permission:Products.Create"));
        productsPermission.AddChild(EcommercePermissions.Products.Edit, L("Permission:Products.Edit"));
        productsPermission.AddChild(EcommercePermissions.Products.Delete, L("Permission:Products.Delete"));


        // ------------------------------------------------------
        // INVENTORY
        // ------------------------------------------------------
        var inventoryPermission = ecommerceGroup.AddPermission(
            EcommercePermissions.Inventory.Default,
            L("Permission:Inventory")
        );
        inventoryPermission.AddChild(EcommercePermissions.Inventory.Create, L("Permission:Inventory.Create"));
        inventoryPermission.AddChild(EcommercePermissions.Inventory.Increase, L("Permission:Inventory.Increase"));
        inventoryPermission.AddChild(EcommercePermissions.Inventory.Decrease, L("Permission:Inventory.Decrease"));
        inventoryPermission.AddChild(EcommercePermissions.Inventory.Delete, L("Permission:Inventory.Delete"));


        // ------------------------------------------------------
        // ORDERS
        // ------------------------------------------------------
        var ordersPermission = ecommerceGroup.AddPermission(
            EcommercePermissions.Orders.Default,
            L("Permission:Orders")
        );

        // Customer permissions
        ordersPermission.AddChild(EcommercePermissions.Orders.Create, L("Permission:Orders.Create"));
        ordersPermission.AddChild(EcommercePermissions.Orders.AddItem, L("Permission:Orders.AddItem"));
        ordersPermission.AddChild(EcommercePermissions.Orders.RemoveItem, L("Permission:Orders.RemoveItem"));
        ordersPermission.AddChild(EcommercePermissions.Orders.UpdateStatusSelf, L("Permission:Orders.UpdateStatusSelf"));

        // Admin permissions
        ordersPermission.AddChild(EcommercePermissions.Orders.Manage, L("Permission:Orders.Manage"));
        ordersPermission.AddChild(EcommercePermissions.Orders.UpdateStatus, L("Permission:Orders.UpdateStatus"));
        ordersPermission.AddChild(EcommercePermissions.Orders.Delete, L("Permission:Orders.Delete"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<EcommerceResource>(name);
    }
}
