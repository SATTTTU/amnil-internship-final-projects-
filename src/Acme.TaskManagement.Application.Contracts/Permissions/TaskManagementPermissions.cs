namespace Acme.TaskManagement.Permissions
{
    public static class TaskManagementPermissions
    {
        public const string GroupName = "TaskManagement";

        public static class Projects
        {
            public const string Default = GroupName + ".Projects";
            public const string Create = Default + ".Create";
            public const string Update = Default + ".Update";
            public const string Delete = Default + ".Delete";
        }

        public static class Tasks
        {
            public const string Default = GroupName + ".Tasks";
            public const string Create = Default + ".Create";
            public const string Update = Default + ".Update";
            public const string Delete = Default + ".Delete";
            public const string Assign = Default + ".Assign";
            public const string UpdateProgress = Default + ".UpdateProgress";
        }
    }
}
