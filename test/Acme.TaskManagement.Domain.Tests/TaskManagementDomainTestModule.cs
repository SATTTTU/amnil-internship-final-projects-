using Volo.Abp.Modularity;

namespace Acme.TaskManagement;

[DependsOn(
    typeof(TaskManagementDomainModule),
    typeof(TaskManagementTestBaseModule)
)]
public class TaskManagementDomainTestModule : AbpModule
{

}
