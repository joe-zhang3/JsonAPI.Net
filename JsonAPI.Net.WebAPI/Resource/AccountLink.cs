using System;
using JsonAPI.Net;

namespace JsonAPI.Net.WebAPI.Resource
{
    public class AccountLink : JaComplexLink
    {
        public AccountLink(){}
        public AccountLink(string name)
        {
            OfName(name);
        }

        public string Method { get; set; }
        public string Test { get; set; }
    }
}
