using System;
using System.Web;

public partial class Error : System.Web.UI.Page
{
    protected int statusCode = 200;
    protected string statusText = "OK";

    protected void Page_Load(object sender, EventArgs e)
    {
        var lastError = Server.GetLastError();
        if (lastError != null)
        {
            EasyGoal.Common.LogException(lastError.GetBaseException(), true);
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

}