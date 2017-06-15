using System;
namespace JsonAPI.Net
{
    public class Constants
    {
        public const string KEY_RELATIONSHIPS = "relationships";

        public const string ACTION_TEMPLATE = "action_template";
            

        public const string MEDIA_TYPE = "application/vnd.api+json";
        public const string REF = "ref";

        public const string DEFAULT_TEMPLATE_PATH = "Templates";

        public const string ERROR_TEMPLATE_NAME = "Error";
		public const string DEFAULT_ERROR_TEMPLATE = @"
        {
            'status' : '',
            'title' : '',
            'detail' : '',
        } ";

        public const string MASTER_TEMPLATE_NAME = "Master";
        public const string DEFAULT_MASTER_TEMPLATE = @"
            {
                'data' : '',
                'links' : '',
                'meta' : '',
                'included' : '',
                'errors' : ''
            }";
        public const string RELATIONSHIP_TEMPLATE_NAME = "Relationship";
        public const string DEFAULT_RELATIONSHIP_TEMPLATE = @"
        {
            'links' : '{{related_links}}',
            'data' : {'type' :'', 'id': ''}
        } ";

    }
}
