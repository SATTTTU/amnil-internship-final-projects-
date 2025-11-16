using Xunit;

namespace Acme.TaskManagement.EntityFrameworkCore;

[CollectionDefinition(TaskManagementTestConsts.CollectionDefinitionName)]
public class TaskManagementEntityFrameworkCoreCollection : ICollectionFixture<TaskManagementEntityFrameworkCoreFixture>
{

}
