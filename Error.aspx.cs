using System;
using System.Web;

public partial class Error : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        var lastError = Server.GetLastError();
        if (lastError != null)
        {
            Exception ex = Server.GetLastError().GetBaseException();
            string errorTime = string.Empty;
            string erroraddr = string.Empty;
            string errorMessage = string.Empty;
            string errorsource = string.Empty;
            string errorStackTrace = string.Empty;
            errorTime = "发生时间: " + System.DateTime.Now.ToString();
            erroraddr = "发生异常页: " + Request.Url.ToString();
            errorMessage = "异常信息: " + ex.Message;
            errorStackTrace = "堆栈信息:" + ex.StackTrace;

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

                    string ip = "用户IP:" + Request.UserHostAddress;
                    string line = "-----------------------------------------------------";

                    string log = "<p style='font-size:9pt;'><br />" + line + "<br /><font color=red>" + errorTime + "&nbsp;&nbsp;" + erroraddr + "</font><br /><font color=green>" + "<br/>" + ip + errorMessage + "<br />" + errorsource + "<br />" + errorStackTrace.Replace("\r\n", "<br />") + "</font></p>";
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
                Response.Write(string.Format("{0},{1}", errorCode, Response.StatusDescription));
            }
        }

    }
}