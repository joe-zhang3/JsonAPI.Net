using System;
using Newtonsoft.Json.Linq;

namespace JsonAPI.Net
{
    public interface IBuilder
    {
        JaBuilderType BuilderType { get; }
        JToken Build(JaBuilderContext context);
    }
}
