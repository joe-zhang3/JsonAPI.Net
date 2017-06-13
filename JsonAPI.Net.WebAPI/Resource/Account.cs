﻿using System;
using Newtonsoft.Json;
using JsonAPI.Net;

namespace FlexibleJsonAPI.WebAPI.Resource
{
    /// <summary>
    /// types/
    /// </summary>
    public class Account : JaResource
    {
        public Account(){
            OfLinks("/customers/accounts")
                .OfTemplate("Accounts");
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
    }
}
