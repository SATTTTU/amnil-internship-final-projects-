using System.Threading.Tasks;
using Shouldly;
using Xunit;

namespace Acme.Ecommerce.Pages;

public class Index_Tests : EcommerceWebTestBase
{
    [Fact]
    public async Task Welcome_Page()
    {
        var response = await GetResponseAsStringAsync("/");
        response.ShouldNotBeNull();
    }
}
