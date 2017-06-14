using System;
using System.Reflection;

namespace JsonAPI.Net
{
    public static class TypeExtensions
    {
        public static bool IsPrimitive(this Type type){
            TypeInfo ti = type.GetTypeInfo();
            return ti.IsPrimitive || ti.IsValueType || (type == typeof(string));
        }

        public static IBuilder GetBuilder(this Type type, bool buildingRelationship = false){
            return JaBuilderFactory.GetBuilder(type, buildingRelationship);
        }
    }
}
