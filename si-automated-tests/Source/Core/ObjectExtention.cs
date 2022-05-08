using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;

namespace si_automated_tests.Source.Core
{
    public static class ObjectExtention
    {
        public static List<T> DataReaderMapToList<T>(IDataReader dr)
        {
            List<T> list = new List<T>();
            T obj = default(T);
            while (dr.Read())
            {
                obj = Activator.CreateInstance<T>();
                foreach (PropertyInfo prop in obj.GetType().GetProperties())
                {
                    if (dr.HasColumn(prop.Name) && !object.Equals(dr[prop.Name], DBNull.Value))
                    {
                        prop.SetValue(obj, dr[prop.Name], null);
                    }
                }
                list.Add(obj);
            }
            return list;
        }

        public static bool HasColumn(this IDataRecord dr, string columnName)
        {
            for (int i = 0; i < dr.FieldCount; i++)
            {
                if (dr.GetName(i).Equals(columnName, StringComparison.InvariantCultureIgnoreCase))
                    return true;
            }
            return false;
        }

        public static int AsInteger(this object inputObject)
        {
            int result;
            if (Int32.TryParse(inputObject.AsString(), out result))
            {
                return result;
            }
            else
            {
                return 0;
            }
        }

        public static int? AsInteger(this object inputObject, bool flag = true)
        {
            if (inputObject == null || string.IsNullOrEmpty(inputObject.AsString())) return null;
            return inputObject.AsInteger();
        }

        public static string AsString(this String inputString)
        {
            if (string.IsNullOrEmpty(inputString)) return "";
            return inputString;
        }

        public static string AsString(this Object inputObject, string defaultValue = "")
        {
            if (inputObject == null || inputObject == DBNull.Value) return defaultValue;
            return inputObject.ToString();
        }

        public static float AsFloat(this Object inputObject)
        {
            float result;
            if (float.TryParse(inputObject.AsString(), out result))
            {
                return result;
            }
            else
            {
                return 0;
            }
        }

        public static float? AsFloat(this Object inputObject, bool flag = true)
        {
            if (inputObject == null || string.IsNullOrEmpty(inputObject.AsString())) return null;
            return inputObject.AsFloat();
        }

        public static double AsDouble(this Object inputObject)
        {
            double result;
            if (double.TryParse(inputObject.AsString(), out result))
            {
                return result;
            }
            else
            {
                return 0;
            }
        }

        public static decimal AsDecimal(this Object inputObject)
        {
            decimal result;
            if (decimal.TryParse(inputObject.AsString(), out result))
            {
                return result;
            }
            else
            {
                return 0;
            }
        }

        public static double? AsDouble(this Object inputObject, bool flag = true)
        {
            if (inputObject == null || string.IsNullOrEmpty(inputObject.AsString())) return null;
            return inputObject.AsDouble();
        }

        public static int AsInt(this Boolean value)
        {
            if (value == true)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

        public static bool AsBool(this object value)
        {
            if (value == null)
            {
                return false;
            }

            if (value is int || value is Int16 || value is Int32 || value is Int64)
            {
                return (int)value == 1;
            }
            else if (value is double || value is Double)
            {
                return (double)value == 1;
            }
            else if (value is float)
            {
                return (float)value == 1;
            }
            else if (value is string || value is String)
            {
                return string.Equals(value.AsString().ToLower(), "true");
            }
            else if (value is bool || value is Boolean)
            {
                return (bool)value;
            }
            else
            {
                throw new NotSupportedException("Not supported for type: " + value.GetType().AsString());
            }
        }

        public static bool AsBool(this object value, bool defaulValue)
        {
            bool result = defaulValue;
            try
            {
                result = value.AsBool();
            }
            catch
            {
                result = defaulValue;
            }
            return result;
        }

        public static IEnumerable<DateTime> EachDay(DateTime from, DateTime thru)
        {
            for (var day = from.Date; day.Date <= thru.Date; day = day.AddDays(1))
                yield return day;
        }
    }
}
