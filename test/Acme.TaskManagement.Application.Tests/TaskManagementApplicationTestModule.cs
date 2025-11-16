using Volo.Abp.Modularity;

namespace Acme.TaskManagement;

[DependsOn(
    typeof(TaskManagementApplicationModule),
    typeof(TaskManagementDomainTestModule)
)]
public class TaskManagementApplicationTestModule : AbpModule
{

}
