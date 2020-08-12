using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace ChessTests
{
    public class EnumHelper
    {
        public static List<T> GetItems<T>(bool excludeNone = false) where T : struct, IConvertible
        {
            if (!typeof(T).IsEnum)
            {
                throw new ArgumentException("T must be an enum type.");
            }

            int noneValue = -1;
            if (excludeNone)
            {
                noneValue = GetNoneValue<T>();
            }

            List<T> list = new List<T>();
            foreach (T value in Enum.GetValues(typeof(T)))
            {
                if (noneValue > -1 && noneValue == GetValue<T>(value)) { continue; }
                list.Add(value);
            }

            return list;
        }

        public static int GetValue<T>(T value) where T : struct, IConvertible
        {
            Type t = typeof(T);
            if (!t.IsEnum)
            {
                throw new ArgumentException("T must be an enum type.");
            }

            return value.ToInt32(CultureInfo.InvariantCulture.NumberFormat);
        }
        public static int GetNoneValue<T>() where T : struct, IConvertible
        {
            if (!typeof(T).IsEnum)
            {
                throw new ArgumentException("T must be an enum type.");
            }

            if (Enum.TryParse("None", true, out T noneValue))
            {
                return GetValue<T>(noneValue);
            }
            else
            {
                return -1;
            }
        }
    }
}
