using System;
using System.Reflection;
using System.Collections;
using Newtonsoft.Json.Linq;
using JsonAPI.Net.Attributes;

namespace JsonAPI.Net.Extensions
{
    public static class ObjectExtension
    {
        public static JValue GenerateJValue(this object obj, bool isId){
            return isId ? new JValue(obj.ToString()) : new JValue(obj);
        }

        public static string GetTemplateName(this object obj){

            string templateName = null;
            if(obj is IEnumerable){
                foreach(var o in (IEnumerable)obj){
                    templateName = GetTemplateInternal(o);
                    break;
                }

            }else{
                return GetTemplateInternal(obj);
			}

            return templateName;
        }

        private static string GetTemplateInternal(object obj){
            foreach (var ca in obj.GetType().GetTypeInfo().GetCustomAttributes())
			{
				JaResourceAttribute jar = ca as JaResourceAttribute;

				if (jar != null)
				{
                    return jar.Template;
				}
			}

            return null;
        }
    }
}
