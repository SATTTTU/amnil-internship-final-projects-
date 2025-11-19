namespace Acme.Ecommerce.Permissions;

public static class EcommercePermissions
{
    public const string GroupName = "Ecommerce";

    //Add your own permission names.
    public static class Categories
    {
        public const string Default = GroupName + ".Categories";
        public const string Create = Default + ".Create";
        public const string Edit = Default + ".Edit";
        public const string Delete = Default + ".Delete";
    }
    public static class Products
    {
        public const string Default = GroupName + ".Products";
        public const string Create = Default + ".Create";
        public const string Edit = Default + ".Edit";
        public const string Delete = Default + ".Delete";
    }
    public static class Inventory
    {
        public const string Default = GroupName + ".Inventory";
        public const string Create = Default + ".Create";
        public const string Increase = Default + ".Increase";
        public const string Decrease = Default + ".Decrease";
        public const string Delete = Default + ".Delete";
    }
    public static class Orders
    {
        public const string Default = GroupName + ".Orders";

        // Customer-level
        public const string Create = Default + ".Create";      
        public const string AddItem = Default + ".AddItem";     
        public const string RemoveItem = Default + ".RemoveItem";
        public const string UpdateStatusSelf = Default + ".UpdateStatus.Self";

        // Admin-level
        public const string Manage = Default + ".Manage";        // admin full access
        public const string UpdateStatus = Default + ".UpdateStatus";
        public const string Delete = Default + ".Delete";
    }
}