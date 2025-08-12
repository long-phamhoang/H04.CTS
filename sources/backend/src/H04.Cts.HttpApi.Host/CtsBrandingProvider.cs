using H04.Cts.Localization;
using Microsoft.Extensions.Localization;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Ui.Branding;

namespace H04.Cts;

[Dependency(ReplaceServices = true)]
public class CtsBrandingProvider : DefaultBrandingProvider
{
    private IStringLocalizer<CtsResource> _localizer;

    public CtsBrandingProvider(IStringLocalizer<CtsResource> localizer)
    {
        _localizer = localizer;
    }

    public override string AppName => _localizer["AppName"];
}