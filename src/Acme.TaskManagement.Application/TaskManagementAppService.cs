using System;
using System.Collections.Generic;
using System.Text;
using Acme.TaskManagement.Localization;
using Volo.Abp.Application.Services;

namespace Acme.TaskManagement;

/* Inherit your application services from this class.
 */
public abstract class TaskManagementAppService : ApplicationService
{
    protected TaskManagementAppService()
    {
        LocalizationResource = typeof(TaskManagementResource);
    }
}
