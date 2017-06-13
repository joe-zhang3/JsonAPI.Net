using System;
using System.Reflection;
using Newtonsoft.Json.Linq;

namespace JsonAPI.Net
{
    public interface IResource 
    {
        JToken Build(JaBuilder builder);
    }
}
   
