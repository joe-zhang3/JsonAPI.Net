﻿using System;
using JsonAPI.Net;
using System.Collections.Generic;

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
            set{
                StateId = int.Parse(value);
            }
        }

        public int StateId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
    }
}
