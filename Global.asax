<%@ Application Language="C#" %>

<script RunAt="server">

  void Application_Start(object sender, EventArgs e)
  {
    // 在应用程序启动时运行的代码
  }

  void Application_End(object sender, EventArgs e)
  {
    //  在应用程序关闭时运行的代码

  }

  void Application_Error(object sender, EventArgs e)
  {
    // 在出现未处理的错误时运行的代码
    Server.Transfer("~/error.aspx");
  }

  void Session_Start(object sender, EventArgs e)
  {
    // 在新会话启动时运行的代码
  }

  void Session_End(object sender, EventArgs e)
  {
    // 在会话结束时运行的代码。 
    // 注意: 只有在 Web.config 文件中的 sessionstate 模式设置为
    // InProc 时，才会引发 Session_End 事件。如果会话模式设置为 StateServer
    // 或 SQLServer，则不引发该事件。
  }

  void Application_BeginRequest(object sender, EventArgs e)
  {
    HttpContext.Current.Items["RequestCounter"] = PreciseCounter.Counter;
    PageEvents.Reset();
  }

  void Application_EndRequest(object sender, EventArgs e)
  {
    long currentCounter = PreciseCounter.Counter;
    long requestCounter = (long)HttpContext.Current.Items["RequestCounter"];
    decimal timetaken = PreciseCounter.TimeSpan(requestCounter, currentCounter, TimeUnit.MilliSecond);
    var log = new EasyGoal.Log(decimal.Round(timetaken, 3));
    //log.Insert();
    try
    {
      log.Insert();
    }
    catch (Exception ex)
    {
      string datetime = DateTime.Now.ToString();
      string exactURL = Request.Url.ToString();
      string address = Request.UserHostAddress;
      string message = ex.Message;
      string stackTrace = ex.StackTrace;

      System.IO.StreamWriter writer = null;
      try
      {
        lock (this)
        {
          string path = Server.MapPath("~/ErrorLog/") + DateTime.Now.ToString("yyyyMM");
          string filename = DateTime.Now.ToString("yyyyMMdd") + ".xml";

          if (!System.IO.Directory.Exists(path))
          {
            System.IO.Directory.CreateDirectory(path);
          }

          bool exist = System.IO.File.Exists(path + "/" + filename);

          System.IO.FileInfo file = new System.IO.FileInfo(path + "/" + filename);

          writer = new System.IO.StreamWriter(file.FullName, true, Encoding.UTF8);

          if (!exist)
            writer.WriteLine("<?xml version=\"1.0\" encoding=\"utf-8\"?>");

          string xml = string.Format("<ErrorLog>\r\n  <DateTime>{0}</DateTime>\r\n  <ExactURL>{1}</ExactURL>\r\n  <Address>{2}</Address>\r\n  <Message>{3}</Message>\r\n  <StackTrace>\r\n{4}\n</StackTrace>\r\n</ErrorLog>\r\n", datetime, exactURL, address, message, stackTrace);
          writer.WriteLine(xml);
          //string html = string.Format("<p style=\"color: #000;\">\r\n  <span style=\"color: red;\">DateTime: </span>{0}<br />\r\n  <span style=\"color: red;\">ExactURL: </span>{1}<br />\r\n  <span style=\"color: red;\">Address: </span>{2}<br />\r\n  <span style=\"color: red;\">Message: </span>{3}<br />\r\n  <span style=\"color: red;\">StackTrace: </span><br />\r\n{4}\r\n</p>\r\n<hr />", datetime, exactURL, address, message, stackTrace.Replace("\r\n", "<br />\r\n"));
          //writer.WriteLine(html);

        }
      }
      finally
      {
        if (writer != null)
          writer.Close();
      }
    }
  }

  void Application_PreSendRequestHeaders(object sender, EventArgs e)
  {

  }

  void Application_PreSendRequestContent(object sender, EventArgs e)
  {

  }
       
</script>
