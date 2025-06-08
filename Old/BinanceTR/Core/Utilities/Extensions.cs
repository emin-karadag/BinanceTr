using System;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace BinanceTR.Core.Utilities
{
    public static class Extensions
    {
        public static string GetDisplayName(this Enum enumValue)
        {
            return enumValue.GetType()
                            .GetMember(enumValue.ToString())[0]
                            .GetCustomAttribute<DisplayAttribute>()
                            .GetName();
        }
    }
}
