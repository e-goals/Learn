using System;
using System.Web;

namespace EZGoal
{
    public class ExceptionModule : System.Web.IHttpModule
    {
        public ExceptionModule() { }

        public void Dispose() { }

        public void Init(HttpApplication application)
        {
            application.Error += new EventHandler(Application_Error);
        }

        private void Application_Error(object sender, EventArgs e)
        {
            var current = HttpContext.Current;

            var lastError = current.Server.GetLastError();
            if (lastError == null)
                return;

            EZGoal.Common.LogException(lastError.GetBaseException(), true);

            var httpException = lastError as HttpException;
            if (httpException != null)
            {
                int httpCode = httpException.GetHttpCode();
                current.Response.StatusCode = httpCode;
                current.Server.ClearError();
            }
        }
    }
}