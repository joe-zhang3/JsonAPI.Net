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

            switch (token)
            {
                case JArray array:
                    return array.Count == 0;
                case JObject obj:
                    return !obj.HasValues;
            }

            return false;
        }
     }
}
