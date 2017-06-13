using System;
using System.Reflection;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace JsonAPI.Net
{
  //  public class JaResources : IResource
  //  {
  //      private TypeInfo ti = null;
  //      private readonly IEnumerable<JaResource> resources;

  //      public JaResources(IEnumerable<JaResource> resources)
  //      {
  //          this.resources = resources;
  //          ti = this.resources?.First().GetType().GetTypeInfo();
  //      }

  //      public IEnumerable<JaResource> Resources{
  //          get { return resources; }
  //      }

		//public IDictionary<string, string> Meta { get; set; }

   //     public JaResource FirstOrDefault(){
   //         return resources?.FirstOrDefault();
   //     }

   //     public void Build(JaDocument builder, JProperty property)
   //     {
   //         JToken jFirst = null;
   //         if (property.Value.Type == JTokenType.Array){
			//	jFirst = ((JArray)property.Value).First;
			//}else{
   //             jFirst = property.Value;
			//}

   //         JArray jArray = new JArray();

			//foreach (var r in Resources){
			//	JToken temp = jFirst.DeepClone();

			//	builder.Populate(temp, r, ti);

			//	jArray.Add(temp);
			//}

			//property.Value = jArray;
    //    }
    //}
}
