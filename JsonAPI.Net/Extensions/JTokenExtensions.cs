using System;
using System.Reflection;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace JsonAPI.Net
{
    public static class JTokenExtensions
    {
        public static bool IsEmpty(this JToken token){

            if (token.Type == JTokenType.Null) return true;

            JArray array = token as JArray;

            if (array != null) return array.Count == 0;

            JObject obj = token as JObject;

            if (obj != null) return !obj.HasValues;

            return false;
        }
     }
}
