using Acme.Ecommerce.Samples;
using Xunit;

namespace Acme.Ecommerce.EntityFrameworkCore.Domains;

[Collection(EcommerceTestConsts.CollectionDefinitionName)]
public class EfCoreSampleDomainTests : SampleDomainTests<EcommerceEntityFrameworkCoreTestModule>
{

}
