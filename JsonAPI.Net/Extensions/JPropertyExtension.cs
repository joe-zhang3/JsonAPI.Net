using System;
using Newtonsoft.Json.Linq;
using Humanizer;

namespace JsonAPI.Net
{
    public static class JPropertyExtension
    {
        public static string EvaulationKey(this JProperty property){
			string value = property.Value.ToString();

            if (!value.EvaulationRequired()) return null;
            return value.Length == 0 ? property.Name.GetKeyFromName() : property.Value.ToString().GetEvaulationKey();
        }
    }
}
