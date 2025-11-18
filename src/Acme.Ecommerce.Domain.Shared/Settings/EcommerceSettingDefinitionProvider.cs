using Acme.Ecommerce.Localization;
using Volo.Abp.Localization;
using Volo.Abp.Settings;

namespace Acme.Ecommerce.Settings;

public class EcommerceSettingDefinitionProvider : SettingDefinitionProvider
{
    public override void Define(ISettingDefinitionContext context)
    {
        context.Add(
            new SettingDefinition(
                "Abp.Account.EnableLocalLogin",
                "true",
                isVisibleToClients: true,
                displayName: LocalizableString.Create<EcommerceResource>("Abp.Account.EnableLocalLogin"))
        );
    }
}

