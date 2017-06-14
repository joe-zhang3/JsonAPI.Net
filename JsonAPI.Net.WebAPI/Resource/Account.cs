﻿using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using JsonAPI.Net;

namespace JsonAPI.Net.WebAPI.Resource
{
    /// <summary>
    /// types/
    /// </summary>
    public class Account : JaResource
    {
        public Account()
        {
            OfURL("customers/accounts");
        }

        public override string Id
        {
            get
            {
                return AccountId.ToString();
            }
        }

        public int AccountId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Age { get; set; }

        public State State { get; set; }

        public List<Person> Persons{get;set;}
    }
}
