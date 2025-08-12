using H04.Cts.Localization;
using Volo.Abp.Application.Services;

namespace H04.Cts;

/* Inherit your application services from this class.
 */
public abstract class CtsAppService : ApplicationService
{
    protected CtsAppService()
    {
        LocalizationResource = typeof(CtsResource);
    }
}