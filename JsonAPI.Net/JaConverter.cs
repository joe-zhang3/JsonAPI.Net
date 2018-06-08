
using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http;

namespace JsonAPI.Net
{
    internal class JaConverter : JsonConverter
    {
        private HttpRequestMessage message = null;

        public JaConverter(HttpRequestMessage message){
            this.message = message;
        }

        public override bool CanConvert(Type objectType)
        {
            return true;
        }
        public override bool CanRead => true;

        public override bool CanWrite => true;

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (value == null) return;

            try{
                JaDocument jaDoc = null;

                IEnumerable<IResource> resources = value as IEnumerable<IResource>;

                if(resources == null){
                    jaDoc = value is JaDocument document ? document : new JaDocument((IResource)value);   
                }else{
                    jaDoc = new JaDocument(resources);
                }

                var context = new JaBuilderContext(message);

                jaDoc.Serialize(context).WriteTo(writer);

            }catch(Exception e){
                 throw e;
            }
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}
