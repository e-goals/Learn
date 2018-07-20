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
            Exception ex = Server.GetLastError().GetBaseException();
            string errorTime = string.Empty;
            string errorURL = string.Empty;
            string errorMessage = string.Empty;
            string errorTrace = string.Empty;
            errorTime = "时间: " + DateTime.Now.ToString();
            errorURL = "链接: " + Request.Url.ToString();
            errorMessage = "信息: " + ex.Message;
            errorTrace = "堆栈跟踪:" + ex.StackTrace;

            //独占方式，因为文件只能由一个进程写入.
            System.IO.StreamWriter writer = null;
            try
            {
                lock (this)
                {
                    // 写入日志
                    string path = string.Empty;
                    string filename = DateTime.Now.ToString("yyyyMMdd") + ".html";
                    path = Server.MapPath("~/ErrorLog/") + DateTime.Now.ToString("yyyyMM");
                    //如果目录不存在则创建
                    if (!System.IO.Directory.Exists(path))
                    {
                        System.IO.Directory.CreateDirectory(path);
                    }
                    System.IO.FileInfo file = new System.IO.FileInfo(path + "/" + filename);

                    //文件不存在就创建,true表示追加

                    writer = new System.IO.StreamWriter(file.FullName, true);

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