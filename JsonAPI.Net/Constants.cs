﻿using System;
namespace JsonAPI.Net
{
    public class Constants
    {
        public const string MEDIA_TYPE = "application/vnd.api+json";
        public const string REF = "ref";

        public const string MASTER_TEMPLATE_NAME = "master_template_name";
        public const string JA_BUILDER = "ja_builder";
        public const string DEFAULT_MASTER = @"
            {
                'data' : '',
                'links' : '',
                'meta' : ''
            }";
        
    }
}
