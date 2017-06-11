using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace JsonAPI.Net
{
    public sealed class JaTemplates
    {
        private static IDictionary<string, JObject> templates = JaTemplateScanner.Scan("Templates");

        public static IDictionary<string, JObject> Templates { get { return templates; } }
    }
}
