﻿﻿using System;
using JsonAPI.Net;

namespace JsonAPI.Net.WebAPI.Resource
{
    public class State : JaResource
    {
        public override string Id
        {
            get
            {
                return StateId.ToString();
            }
        }

        public int StateId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
    }
}
