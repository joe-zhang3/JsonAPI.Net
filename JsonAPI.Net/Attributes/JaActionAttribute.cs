using System;
using System.Collections.Generic;
using System.Web.Http.Filters;

namespace JsonAPI.Net
{
    [AttributeUsage(AttributeTargets.Method)]
    public class JaActionAttribute : ActionFilterAttribute
    {
        private string masterTemplate;

        public JaActionAttribute(string masterTemplate){
            this.masterTemplate = masterTemplate;
        }

        public string MasterTemplate { get { return masterTemplate; }}

        public override void OnActionExecuting(System.Web.Http.Controllers.HttpActionContext actionContext)
        {
            if(masterTemplate != null){
                actionContext.Request.Properties.Add(Constants.MASTER_TEMPLATE_NAME, masterTemplate);
            }

            base.OnActionExecuting(actionContext);
        }
    }
}
