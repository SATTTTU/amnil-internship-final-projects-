using Microsoft.AspNetCore.Builder;
using Acme.Ecommerce;
using Volo.Abp.AspNetCore.TestBase;

var builder = WebApplication.CreateBuilder();

builder.Environment.ContentRootPath = GetWebProjectContentRootPathHelper.Get("Acme.Ecommerce.Web.csproj");
await builder.RunAbpModuleAsync<EcommerceWebTestModule>(applicationName: "Acme.Ecommerce.Web" );

public partial class Program
{
}
