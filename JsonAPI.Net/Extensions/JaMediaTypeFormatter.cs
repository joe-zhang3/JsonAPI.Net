using System;
using System.IO;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace JsonAPI.Net
{
    internal class JaMediaTypeFormatter : MediaTypeFormatter
    {
        private HttpRequestMessage message;
        internal JaMediaTypeFormatter(HttpRequestMessage message) : this(){
            this.message = message;
        }

        internal JaMediaTypeFormatter(){
            SupportedMediaTypes.Add(new MediaTypeHeaderValue(Constants.MEDIA_TYPE));
        }

        public override bool CanReadType(Type type)
        {
            return true;
        }
        public override bool CanWriteType(Type type)
        {
            return true;
        }

        public override async Task WriteToStreamAsync(Type type, object value, Stream writeStream, HttpContent content, TransportContext transportContext)
        {
             if (!(value is IResource || value is IEnumerable<IResource>)) await base.WriteToStreamAsync(type, value, writeStream, content, transportContext);

            string result = JsonConvert.SerializeObject(value, Formatting.Indented, new JaConverter(message));

            using(var writter = new StreamWriter(writeStream)){
                await writter.WriteAsync(result);
            }
        }

        public override async Task<object> ReadFromStreamAsync(Type type, Stream readStream, HttpContent content, IFormatterLogger formatterLogger)
        {
			using (var reader = new StreamReader(readStream))
			{
				try
				{
					var json = JToken.Parse(await reader.ReadToEndAsync());
                    return JaResourceBase.Deserialize(json).ToObject(type);
				}
                catch (Exception e)
				{
                    throw e; //debug purpose
				}
			}
        }

        public override MediaTypeFormatter GetPerRequestFormatterInstance(Type type, HttpRequestMessage request, MediaTypeHeaderValue mediaType)
        {
            return new JaMediaTypeFormatter(request);
        }
    }
}
