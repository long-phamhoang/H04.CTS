using System;
using System.ComponentModel;
using System.Linq;

namespace H04.Cts.Utilities;

public static class Utilities
{
    public static string GetEnumDescription(this Enum enumValue)
    {
        if (enumValue == null) return string.Empty;
        var fieldInfo = enumValue.GetType().GetField(enumValue.ToString());
        if (fieldInfo == null) return string.Empty;

        var descriptionAttribute = fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false).FirstOrDefault();
        return descriptionAttribute == null ? string.Empty : ((DescriptionAttribute)descriptionAttribute).Description;
    }

    public static T GetValueFromDescription<T>(string description) where T : Enum
    {
        foreach (var field in typeof(T).GetFields())
        {
            if (Attribute.GetCustomAttribute(field,
            typeof(DescriptionAttribute)) is DescriptionAttribute attribute)
            {
                if (attribute.Description == description)
                    return (T)field.GetValue(null);
            }
            else
            {
                if (field.Name == description)
                    return (T)field.GetValue(null);
            }
        }

        return default;
        // Or throw new ArgumentException("Not found.", nameof(description));
    }
}