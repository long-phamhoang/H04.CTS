using H04.Cts.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace H04.Cts.Controllers;

/* Inherit your controllers from this class.
 */
public abstract class CtsController : AbpControllerBase
{
    protected CtsController()
    {
        LocalizationResource = typeof(CtsResource);
    }
}