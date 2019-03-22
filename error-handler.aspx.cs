using System;
using System.Web;

public partial class ErrorHandler : System.Web.UI.Page
{
    protected int statusCode = 200;
    protected string statusText = "OK";

    protected void Page_Load(object sender, EventArgs e)
    {
        Exception lastError = Server.GetLastError();
        if (lastError == null)
            return;

        EZGoal.Common.LogException(lastError.GetBaseException(), true);

        var httpException = lastError as HttpException;
        if (httpException != null)
        {
            int httpCode = httpException.GetHttpCode();
            Server.ClearError();
            Response.StatusCode = httpCode;
            this.statusCode = httpCode;
            this.statusText = Response.StatusDescription;
        }
    }

}