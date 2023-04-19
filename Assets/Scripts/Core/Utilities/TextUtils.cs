using System.Reflection;
using System;
using UnityEngine;
using System.ComponentModel;

namespace Assets.Scripts.Core.Utilities
{
    public static class TextUtils
    {
        /// <returns>String with boolean value colored Red (false) or Green (true)</returns>
        public static string BoolColor(this bool boolean) => boolean ? boolean.ToString().Color(UnityEngine.Color.green) : boolean.ToString().Color(UnityEngine.Color.red);
        public static string Bold(this string text) => $"<b>{text}</b>";
        public static string Italic(this string text) => $"<i>{text}</i>";
        public static string Color(this string text, Color color) => $"<color=#{ColorUtility.ToHtmlStringRGB(color)}>{text}</color>";
        public static string Size(this string text, float size) => $"<size={size}>{text}</size>";
        public static string GetDescription<T>(this T enumerationValue) where T : struct
        {
            Type type = enumerationValue.GetType();
            if (!type.IsEnum)
            {
                throw new ArgumentException("EnumerationValue must be of Enum type", "enumerationValue");
            }

            //Tries to find a DescriptionAttribute for a potential friendly name
            //for the enum
            MemberInfo[] memberInfo = type.GetMember(enumerationValue.ToString());
            if (memberInfo != null && memberInfo.Length > 0)
            {
                object[] attrs = memberInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);

                if (attrs != null && attrs.Length > 0)
                {
                    //Pull out the description value
                    return ((DescriptionAttribute)attrs[0]).Description;
                }
            }
            //If we have no description attribute, just return the ToString of the enum
            return enumerationValue.ToString();
        }
    }
}