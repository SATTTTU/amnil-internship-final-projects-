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
}