using Acme.TaskManagement.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace Acme.TaskManagement.Permissions;

public class TaskManagementPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        // Create main permission group
        var group = context.AddGroup(TaskManagementPermissions.GroupName, L("Permission:TaskManagement"));

        // PROJECT PERMISSIONS
        var projectPermission = group.AddPermission(TaskManagementPermissions.Projects.Default, L("Permission:Projects"));
        projectPermission.AddChild(TaskManagementPermissions.Projects.Create, L("Permission:Projects.Create"));
        projectPermission.AddChild(TaskManagementPermissions.Projects.Update, L("Permission:Projects.Update"));
        projectPermission.AddChild(TaskManagementPermissions.Projects.Delete, L("Permission:Projects.Delete"));

        // TASK PERMISSIONS
        var taskPermission = group.AddPermission(TaskManagementPermissions.Tasks.Default, L("Permission:Tasks"));
        taskPermission.AddChild(TaskManagementPermissions.Tasks.Create, L("Permission:Tasks.Create"));
        taskPermission.AddChild(TaskManagementPermissions.Tasks.Update, L("Permission:Tasks.Update"));
        taskPermission.AddChild(TaskManagementPermissions.Tasks.Delete, L("Permission:Tasks.Delete"));
        taskPermission.AddChild(TaskManagementPermissions.Tasks.Assign, L("Permission:Tasks.Assign"));
        taskPermission.AddChild(TaskManagementPermissions.Tasks.UpdateProgress, L("Permission:Tasks.UpdateProgress"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<TaskManagementResource>(name);
    }
}
