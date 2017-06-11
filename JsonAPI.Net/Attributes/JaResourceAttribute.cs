using System;
namespace JsonAPI.Net.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class JaResourceAttribute : Attribute
    {
        private string template;

        public JaResourceAttribute(string template)
        {
            this.template = template;
        }

        public string Template { get { return template; } }
    }
}
