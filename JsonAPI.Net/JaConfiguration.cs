using System;
using System.Collections.Generic;

namespace JsonAPI.Net
{
    public class JaConfiguration
    {
        private IList<ICustomBuilder> builders;

        internal IEnumerable<ICustomBuilder> GetBuilders(){
            return builders;
        }
        public string TemplateDirectory { get; set; }

        public JaConfiguration RegisterTypeBuilder(ICustomBuilder builder){
            if(builders == null) builders = new List<ICustomBuilder>();

            builders.Add(builder);
            
            return this;
        }
    }
}
