using Acme.TaskManagement.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace Acme.TaskManagement.Controllers;

/* Inherit your controllers from this class.
 */
public abstract class TaskManagementController : AbpControllerBase
{
    protected TaskManagementController()
    {
        LocalizationResource = typeof(TaskManagementResource);
    }
}
