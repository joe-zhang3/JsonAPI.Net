using System;
using Newtonsoft.Json.Linq;
using JsonAPI.Net.WebAPI.Resource;

namespace JsonAPI.Net.WebAPI
{
    public class IntBuilder : JaCustomBuilder
    {
        public IntBuilder() : base(typeof(int)){
            
        }

        public override JToken Build(JaBuilderContext context)
        {
            return new JValue(context.Value.ToString());
        }
    }


	public class ComplexBuilder : JaCustomBuilder
	{
        public ComplexBuilder() : base(typeof(MyComplexObject))
		{

		}

		public override JToken Build(JaBuilderContext context)
		{
			return new JValue(context.Value.ToString());
		}
	}
}
