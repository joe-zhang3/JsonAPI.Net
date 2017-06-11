
using System;
using System.IO;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Converters;
using System.Reflection;
using JsonAPI.Net.Attributes;
using JsonAPI.Net.Builder;
using JsonAPI.Net.Extensions;
namespace JsonAPI.Net
{
    public class JaConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return true;
        }
        public override bool CanRead
        {
            get
            {
                return true;
            }
        }

        public override bool CanWrite
        {
            get
            {
                return base.CanWrite;
            }
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            string templateName = value.GetTemplateName();

            JObject returnObject = null;

            try{

                JObject jobj = JaTemplates.Templates[templateName];

                JaObjectBuilder jaObject = new JaObjectBuilder(jobj, value);

                returnObject = jaObject.BuildObject();

            }catch(Exception e){
                throw e;
            }

            returnObject?.WriteTo(writer);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }


}
