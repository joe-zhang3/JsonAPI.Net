using System;
using System.Linq;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace JsonAPI.Net
{
    internal static class ObjectExtensions
    {
        public static JToken GenerateJValue(this object obj, bool isId)
        {

            if (obj.GetType() == typeof(JObject))
            {
                return (JObject)obj;
            }

            return isId ? new JValue(obj.ToString()) : new JValue(obj);
        }

        public static T GetItem<T>(this object obj)
        {

            if (obj is IEnumerable<T>)
            {
                return ((IEnumerable<T>)obj).First<T>();
            }
            else
            {
                return (T)obj;
            }

        }
    }
}
