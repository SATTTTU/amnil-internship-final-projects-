using System;
using System.Collections.Generic;
using System.Text;
using Acme.Ecommerce.Localization;
using Volo.Abp.Application.Services;

namespace Acme.Ecommerce;

/* Inherit your application services from this class.
 */
public abstract class EcommerceAppService : ApplicationService
{
    protected EcommerceAppService()
    {
        LocalizationResource = typeof(EcommerceResource);
    }
}
