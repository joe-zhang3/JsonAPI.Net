using System;
namespace JsonAPI.Net
{
    [AttributeUsage(AttributeTargets.Class)]
    public class JaTemplateAttribute : Attribute
    {
        private string template;

        public JaTemplateAttribute(string template)
        {
            this.template = template;
        }

        public string Template { get { return template; } }
    }
}
