
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Xml.Linq;


using System.Xml.Serialization;
using System.IO;
using System.Xml;


namespace SalesApp.Common
{



    public static class CommonHelper
    {







        public static void ConvertSourceToTarget<T, S>(T target, S source)
        {
            var properties = from prop in source.GetType().GetProperties()
                             select prop;

            foreach (PropertyInfo property in properties)
            {
                dynamic propertyValue = property.GetValue(source, null);
                PropertyInfo tProperty = target.GetType().GetProperties().Where(x => x.Name == property.Name).FirstOrDefault();

                if (propertyValue != null && tProperty != null)
                {
                    var propertyType = property.PropertyType.FullName;
                    if (property.PropertyType.IsGenericType && property.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>))
                    {
                        propertyType = property.PropertyType.GetGenericArguments()[0].FullName;
                    }
                    switch (propertyType)
                    {
                        case "System.Int32":
                            {
                                tProperty.SetValue(target, Convert.ToInt32(propertyValue), null);
                                break;
                            }
                        case "System.Nullable[System.Int32]":
                            {
                                tProperty.SetValue(target, Convert.ToInt32(propertyValue), null);
                                break;
                            }
                        case "System.Int64":
                            {
                                tProperty.SetValue(target, Convert.ToInt64(propertyValue), null);
                                break;
                            }
                        case "System.Boolean":
                            {
                                tProperty.SetValue(target, Convert.ToBoolean(Convert.ToInt16(propertyValue)), null);
                                break;
                            }
                        case "System.String":
                            {
                                tProperty.SetValue(target, propertyValue, null);
                                break;
                            }
                        case "System.DateTime":
                            {
                                tProperty.SetValue(target, Convert.ToDateTime(propertyValue), null);
                                break;
                            }
                        case "System.Decimal":
                            {
                                tProperty.SetValue(target, Math.Round(Convert.ToDecimal(propertyValue), 2), null);
                                break;
                            }
                        case "System.Double":
                            {
                                tProperty.SetValue(target, Math.Round(Convert.ToDouble(propertyValue), 2), null);
                                break;
                            }
                        default:
                            {
                                if (!property.PropertyType.Name.Contains("List") && !property.PropertyType.Name.Contains("Collection"))
                                {
                                    Type t = Enum.GetUnderlyingType(property.PropertyType);
                                    switch (t.FullName)
                                    {
                                        case "System.Int16":
                                            {
                                                tProperty.SetValue(target, Convert.ToInt16(propertyValue), null);
                                                break;
                                            }
                                        case "System.Int32":
                                            {
                                                tProperty.SetValue(target, Convert.ToInt32(propertyValue), null);
                                                break;
                                            }
                                    }
                                }
                                break;
                            }
                    }
                }
            }
        }

        /// <summary>
        /// Retrieve the description on the enum, e.g.
        /// [Description("Bright Pink")]
        /// BrightPink = 2,
        /// Then when you pass in the enum, it will retrieve the description
        /// </summary>
        /// <param name="en">The Enumeration</param>
        /// <returns>A string representing the friendly name</returns>
        public static string GetDescription(this Enum en)
        {
            Type type = en.GetType();

            MemberInfo[] memInfo = type.GetMember(en.ToString());

            if (memInfo != null && memInfo.Length > 0)
            {
                object[] attrs = memInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);

                if (attrs != null && attrs.Length > 0)
                {
                    return ((DescriptionAttribute)attrs[0]).Description;
                }
            }

            return en.ToString();
        }


        public static IList ToList(this Type type)
        {
            if (type == null)
            {
                throw new ArgumentNullException("type");
            }

            ArrayList list = new ArrayList();
            Array enumValues = Enum.GetValues(type);

            foreach (Enum value in enumValues)
            {
                list.Add(new KeyValuePair<Enum, string>(value, GetDescription(value)));
            }

            return list;
        }

        public static string Escape(this string input)
        {
            char[] toEscape = "\0\x1\x2\x3\x4\x5\x6\a\b\t\n\v\f\r\xe\xf\x10\x11\x12\x13\x14\x15\x16\x17\x18\x19\x1a\x1b\x1c\x1d\x1e\x1f\x2C\"\\".ToCharArray();
            string[] literals = @"\0,\x0001,\x0002,\x0003,\x0004,\x0005,\x0006,\a,\b,\t,\n,\v,\f,\r,\x000e,\x000f,\x0010,\x0011,\x0012,\x0013,\x0014,\x0015,\x0016,\x0017,\x0018,\x0019,\x001a,\x001b,\x001c,\x001d,\x001e,\x001f\x002C".Split(new char[] { ',' });

            int i = input.IndexOfAny(toEscape);
            if (i < 0) return input;

            var sb = new System.Text.StringBuilder(input.Length + 5);
            int j = 0;
            do
            {
                sb.Append(input, j, i - j);
                var c = input[i];
                if (c < 0x20) sb.Append(literals[c]); else sb.Append(@"\").Append(c);
            } while ((i = input.IndexOfAny(toEscape, j = ++i)) > 0);

            return sb.Append(input, j, input.Length - j).ToString();
        }

        public static int ModifyToInt(this string str)
        {
            int num = 0;
            if (!string.IsNullOrEmpty(str))
            {
                num = int.Parse(str);
            }
            return num;

        }

        public static long ModifyToLong(this string str)
        {
            long num = 0;
            if (!string.IsNullOrEmpty(str))
            {
                num = long.Parse(str);
            }
            return num;

        }

        public static double ModifyToDouble(this string str)
        {
            double num = 0;
            if (!string.IsNullOrEmpty(str))
            {
                num = double.Parse(str);
            }
            return num;
        }

        public static decimal ModifyToDecimal(this string str)
        {
            decimal num = 0;
            if (!string.IsNullOrEmpty(str))
            {
                num = Convert.ToDecimal(str);
            }
            return num;

        }

        public static bool ModifyToBool(this string flag)
        {
            bool result = false;
            if (!string.IsNullOrEmpty(flag) && (flag.ToUpper() == "true".ToUpper() || flag == "1"))
            {
                result = true;
            }
            return result;
        }

        public static string IsString(this string str)
        {
            string result = string.Empty;
            if (!string.IsNullOrEmpty(str))
            {
                result = str;
            }
            return result;

        }

        public static DateTime ModifyToDateTime(this string strDate, string format)
        {
            return DateTime.ParseExact(strDate.Trim(), format, CultureInfo.InvariantCulture);
        }

        public static string ModifyDateFormat(this string strDate, string oldFormat, string newFormat)
        {
            string str = string.Empty;
            if (!string.IsNullOrEmpty(strDate))
            {
                DateTime dte = DateTime.ParseExact(strDate.Trim(), oldFormat, CultureInfo.InvariantCulture);
                return dte.ToString(newFormat);
            }
            return str;
        }

        public static DateTime IsDateUTC(this DateTime strDate)
        {
            if (strDate != null)
            {
                strDate = strDate.ToUniversalTime();
            }
            return strDate;
        }

        public static string IsBlank(this string str)
        {
            string result = string.Empty;
            if (!string.IsNullOrEmpty(str))
            {
                result = str;
            }
            else
            {
                result = "-";
            }
            return result;

        }

        public static string IsBlankInt(this int data)
        {
            string result = string.Empty;
            if (data != 0)
            {
                result = data.ToString();
            }
            else
            {
                result = "-";
            }
            return result;

        }

        public static double ToDouble(this string item)
        {
            IFormatProvider culture = Thread.CurrentThread.CurrentCulture;
            return Convert.ToDouble(item, culture);
        }

        public static decimal ToDecimal(this string item)
        {
            IFormatProvider culture = Thread.CurrentThread.CurrentCulture;
            return Convert.ToDecimal(item, culture);
        }

        public static int ConvetToNum(this string strNo)
        {
            int result = 0;
            if (!string.IsNullOrEmpty(strNo))
            {
                bool isNumeric = int.TryParse(strNo, out result);
            }
            return result;

        }

        public static string RemoveSpace(this string str)
        {
            if (!string.IsNullOrEmpty(str))
            {
                Regex.Replace(str, "[ \n\r\t]", "");
            }
            return str;

        }

        public static string cleanForJSON(this string s)
        {
            if (s == null || s.Length == 0)
            {
                return "";
            }

            char c = '\0';
            int i;
            int len = s.Length;
            StringBuilder sb = new StringBuilder(len + 4);
            //String t;

            for (i = 0; i < len; i += 1)
            {
                c = s[i];
                switch (c)
                {
                    case '/':
                        sb.Append('\\');
                        sb.Append(c);
                        break;
                    case '\b':
                        sb.Append("\\b");
                        break;
                    case '\t':
                        sb.Append("\\t");
                        break;
                    case '\n':
                        sb.Append("\\n");
                        break;
                    case '\f':
                        sb.Append("\\f");
                        break;
                    case '\r':
                        sb.Append("\\r");
                        break;
                    default:
                        int cint = Convert.ToInt32(c);
                        if (cint >= 123 || (cint >= 32 && cint <= 46) || (cint >= 58 && cint <= 64) || ((cint >= 91 && cint <= 96) && cint != 92))
                        {
                            sb.Append(String.Format("\\u{0:x4} ", cint).Trim());
                        }
                        else
                        {
                            sb.Append(c);
                        }
                        break;
                }
            }
            return sb.ToString();
        }

        public static string cleanFormJSON(this string str)
        {
            if (str == null || str.Length == 0)
            {
                return "";
            }
            str = Regex.Replace(str, @"\\u(?<Value>[a-zA-Z0-9]{4})",
         m =>
         {
             return ((char)int.Parse(m.Groups["Value"].Value, NumberStyles.HexNumber)).ToString();
         });

            str = str.Replace("\\n", "\n");
            str = str.Replace("\\b", "\b");
            str = str.Replace("\\f", "\f");
            str = str.Replace("\\t", "\t");
            str = str.Replace("\\r", "\r");
            str = str.Replace("\\", "/");
            return str.ToString();

        }

        public static string GetFileName()
        {
            Guid guid = Guid.NewGuid();
            if (guid == Guid.Empty)
            {
                guid = Guid.NewGuid();
                if (guid == Guid.Empty)
                {
                    guid = Guid.NewGuid();
                }
            }
            return (DateTime.Now.ToString("yyyyMMdd", CultureInfo.GetCultureInfo("en-US")) + guid.ToString().Replace("-", string.Empty));
        }



        public static TimeSpan ToTime(this string strTime)
        {
            return TimeSpan.Parse(strTime);
        }

 



        public static string ToStr2Place(this decimal point, string culture)
        {
            return point.ToString("F2", CultureInfo.CreateSpecificCulture(culture));
        }

        public static string ToStr3Place(this decimal point,string culture)
        {
            return point.ToString("F3", CultureInfo.CreateSpecificCulture(culture));
        }













    }

   


}










