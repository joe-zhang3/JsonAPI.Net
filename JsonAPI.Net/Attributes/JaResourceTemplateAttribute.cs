using System;
using System.Collections.Generic;
using System.Web.Http.Filters;

namespace JsonAPI.Net
{
    [AttributeUsage(AttributeTargets.Method)]
    public class JaResourceTemplateAttribute : ActionFilterAttribute
    {
        private string template;

        public JaResourceTemplateAttribute(string masterTemplate){
            this.template = masterTemplate;
        }

        public override void OnActionExecuting(System.Web.Http.Controllers.HttpActionContext actionContext)
        {
            if(template != null){
                actionContext.Request.Properties.Add(Constants.ACTION_TEMPLATE, template);
            }

            base.OnActionExecuting(actionContext);
        }
    }
}
