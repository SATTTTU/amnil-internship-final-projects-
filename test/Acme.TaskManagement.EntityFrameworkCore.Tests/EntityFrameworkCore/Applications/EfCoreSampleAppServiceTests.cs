using Acme.TaskManagement.Samples;
using Xunit;

namespace Acme.TaskManagement.EntityFrameworkCore.Applications;

[Collection(TaskManagementTestConsts.CollectionDefinitionName)]
public class EfCoreSampleAppServiceTests : SampleAppServiceTests<TaskManagementEntityFrameworkCoreTestModule>
{

}
