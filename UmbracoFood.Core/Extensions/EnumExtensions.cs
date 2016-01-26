using System;
using System.ComponentModel;
using System.Reflection;

namespace UmbracoFood.Core.Extensions
{
    public static class EnumExtensions
    {
        public static string GetDescription(this Enum element)
        {
            FieldInfo fieldInfo = element.GetType().GetField(element.ToString());

            var attributes = (DescriptionAttribute[])fieldInfo.GetCustomAttributes(typeof (DescriptionAttribute), false);

            if (attributes.Length > 0)
            {
                return attributes[0].Description;
            }

            return element.ToString();
        }
    }
}