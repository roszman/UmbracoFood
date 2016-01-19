using System.Data.SqlClient;
using System.Net;
using System.Net.Http;
using System.Web.Http.Filters;
using Autofac.Integration.WebApi;
using Umbraco.Core.Logging;
using Umbraco.Web.WebApi;

namespace UmbracoFood.Filters
{
    public class WebApiExceptionFilter : ExceptionFilterAttribute, IAutofacExceptionFilter
    {
        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            LogHelper.Error<UmbracoApiController>("There was an exception.", actionExecutedContext.Exception);

            if (actionExecutedContext.Exception is SqlException)
            {
                HandleSqlException(actionExecutedContext);
                return;
            }

            if (actionExecutedContext.Exception != null)
            {
                actionExecutedContext.Response = new HttpResponseMessage()
                {
                    ReasonPhrase = "There was an Exception. Contact the administrator.",
                    StatusCode = HttpStatusCode.InternalServerError
                };
            }

            base.OnException(actionExecutedContext);
        }

        private void HandleSqlException(HttpActionExecutedContext actionExecutedContext)
        {
            actionExecutedContext.Response = new HttpResponseMessage(HttpStatusCode.InternalServerError)
            {
                ReasonPhrase = "There was a SqlException"
            };
        }
    }
}