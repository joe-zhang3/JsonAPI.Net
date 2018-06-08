using System;
using System.Runtime.CompilerServices;
using Newtonsoft.Json.Linq;
using Humanizer;

namespace JsonAPI.Net
{
    public class JaLink : ILink
    {
        private string name;

        public Uri Href { get; set; }

        public string Name => name ?? GetType().Name.ToLower().Pluralize();

        public void OfName(string name) =>this.name = name;

        public virtual JToken Serialize(JaBuilderContext context){
            throw new NotImplementedException();
        }

        public JContainer GetContainer(){
            return new JObject();
        }
    }
}
