using Volo.Abp.Modularity;

namespace Acme.TaskManagement;

public abstract class TaskManagementApplicationTestBase<TStartupModule> : TaskManagementTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}
