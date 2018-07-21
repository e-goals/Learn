using System;
using System.Web;
using System.IO;

public partial class Error : System.Web.UI.Page
{
    protected int statusCode = 200;
    protected string statusText = "OK";

    protected void Page_Load(object sender, EventArgs e)
    {
        var lastError = Server.GetLastError();
        if (lastError != null)
        {
            Exception ex = Server.GetLastError().GetBaseException();
            string errorTime = string.Empty;
            string errorURL = string.Empty;
            string errorMessage = string.Empty;
            string errorTrace = string.Empty;
            errorTime = "时间: " + DateTime.Now.ToString();
            errorURL = "链接: " + Request.Url.ToString();
            errorMessage = "信息: " + ex.Message;
            errorTrace = "堆栈跟踪:" + ex.StackTrace;

            StreamWriter writer = null;
            try
            {
                lock (this)
                {
                    // 写入日志
                    string path = string.Empty;
                    string filename = DateTime.Now.ToString("yyyyMMdd") + ".html";
                    path = Server.MapPath("~/ErrorLog/") + DateTime.Now.ToString("yyyyMM");

                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                    FileInfo file = new FileInfo(path + "/" + filename);

                    writer = new StreamWriter(file.FullName, true);

                    string ip = "ClientIP:" + Request.UserHostAddress;
                    string line = "-----------------------------------------------------";

                    string log = "<p style='font-size:9pt;'><br />" + line + "<br /><font color=red>" + errorTime + "&nbsp;&nbsp;" + errorURL + "</font><br /><font color=green>" + "<br/>" + ip + errorMessage + "<br />" + errorTrace.Replace("\r\n", "<br />") + "</font></p>";
                    writer.WriteLine(log);

                }
            }
            finally
            {
                if (writer != null)
                    writer.Close();
            }
            var httpError = lastError as HttpException;
            if (httpError != null)
            {
                int errorCode = httpError.GetHttpCode();
                Server.ClearError();
                Response.StatusCode = errorCode;
                this.statusCode = errorCode;
                this.statusText = Response.StatusDescription;
            }
        }

    }
}