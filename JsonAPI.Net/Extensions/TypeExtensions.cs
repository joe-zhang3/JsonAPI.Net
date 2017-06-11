using System;
using System.Reflection;

namespace JsonAPI.Net.Extensions
{
    public static class TypeExtensions
    {
        public static bool IsPrimitive(this Type type){
            TypeInfo ti = type.GetTypeInfo();
            return ti.IsPrimitive || ti.IsValueType || (type == typeof(string));
        }
    }
}
