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
            get{
                return AccountId.ToString();
            }
            set { AccountId = int.Parse(value); }
        }

        public int AccountId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public DateTime Date { get; set; }
        public MyComplexObject Complex { get; set; }

        public State State { get; set; }

        public List<Person> Persons{get;set;}
    }

    public class MyComplexObject{
        public string MyComplexObjectProperty1 { get; set; }
        public string MyComplexObjectProperty2 { get; set; }

        public override string ToString()
        {
            return string.Format("I am a complex object");
        }
    }
}
