using Volo.Abp.Settings;

namespace H04.Cts.Settings;

public class CtsSettingDefinitionProvider : SettingDefinitionProvider
{
    public override void Define(ISettingDefinitionContext context)
    {
        //Define your own settings here. Example:
        //context.Add(new SettingDefinition(CtsSettings.MySetting1));
    }
}