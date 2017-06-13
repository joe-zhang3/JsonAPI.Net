using System;
using JsonAPI.Net;

namespace JsonAPI.Net.WebAPI.Resource
{
    public class Person : JaResource
    {
        public Person(){
            OfType("persons");
        }

        public override string Id
        {
            get
            {
                return PersonId.ToString();
            }
        }

        public int PersonId { get; set; }
        public string Name { get; set; }
    }
}
