using System;
using Newtonsoft.Json.Linq;
using Humanizer;

namespace JsonAPI.Net
{
    public static class JPropertyExtension
    {
        public static string EvaulationKey(this JProperty property){
			string value = property.Value.ToString();

			if (value.EvaulationRequired())
			{
                if(value.Length == 0){
                    return property.Name.GetKeyFromName();                          
                }else{
                    return property.Value.ToString().GetEvaulationKey();
                }
			}

            return null;
        }
    }
}
