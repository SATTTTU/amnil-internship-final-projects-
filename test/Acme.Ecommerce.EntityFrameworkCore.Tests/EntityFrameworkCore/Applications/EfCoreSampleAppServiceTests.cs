using Acme.Ecommerce.Samples;
using Xunit;

namespace Acme.Ecommerce.EntityFrameworkCore.Applications;

[Collection(EcommerceTestConsts.CollectionDefinitionName)]
public class EfCoreSampleAppServiceTests : SampleAppServiceTests<EcommerceEntityFrameworkCoreTestModule>
{

}
