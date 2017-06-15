using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
namespace JsonAPI.Net
{
    /// <summary>
    /// Part of the error handling code is copied from Saule. 
    /// </summary>
    public class JaDelegatingHandler : DelegatingHandler
    {
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var result = await base.SendAsync(request, cancellationToken);
            var hasMediaType = request.Headers.Accept.Any(x => x.MediaType == Constants.MEDIA_TYPE);
			//TODO Add error handling here.
            var statusCode = (int)result.StatusCode;
			if (!hasMediaType || (statusCode >= 400 && statusCode < 500)){
				// probably malformed request or not found
				return result;
			}

            var content = result.Content as ObjectContent;

            var error = content?.Value as HttpError;

            if (error != null){
                JaError je = new JaError(error);
                je.StatusCode = result.StatusCode;
                request.Properties.Add(Constants.HAS_ERROR, je);
            }

            return result;
        }
    }
}