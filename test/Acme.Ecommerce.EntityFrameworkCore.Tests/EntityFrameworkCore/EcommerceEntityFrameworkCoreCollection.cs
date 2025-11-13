using Xunit;

namespace Acme.Ecommerce.EntityFrameworkCore;

[CollectionDefinition(EcommerceTestConsts.CollectionDefinitionName)]
public class EcommerceEntityFrameworkCoreCollection : ICollectionFixture<EcommerceEntityFrameworkCoreFixture>
{

}
