using System;
using System.Reflection;
using System.Collections.Generic;
using System.Web.Http.Filters;

namespace JsonAPI.Net
{
	[AttributeUsage(AttributeTargets.Method)]
	public class JaBuilderAttribute : ActionFilterAttribute
	{
		private Type builderType;

		public JaBuilderAttribute(Type builderType)
		{
			this.builderType = builderType;
		}

		public override void OnActionExecuting(System.Web.Http.Controllers.HttpActionContext actionContext)
		{
			if (builderType != null)
			{
                JaBuilder jb = Activator.CreateInstance<JaBuilder>();
                                         
				actionContext.Request.Properties.Add(Constants.MASTER_TEMPLATE_NAME, jb);
			}

			base.OnActionExecuting(actionContext);
		}
	}
}
