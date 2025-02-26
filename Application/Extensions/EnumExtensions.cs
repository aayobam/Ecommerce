using System.ComponentModel.DataAnnotations;
using System.Reflection;

public static class EnumExtensions
{
    public static string GetDisplayName(this Enum enumValue)
    {
        var enumType = enumValue.GetType();
        var member = enumType.GetMember(enumValue.ToString());

        if (member.Length > 0)
        {
            var displayAttribute = member[0].GetCustomAttribute<DisplayAttribute>();
            if (displayAttribute != null)
            {
                return displayAttribute.Name;
            }
        }
        return enumValue.ToString();
    }
}
