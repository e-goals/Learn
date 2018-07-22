using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Xml.Linq;

namespace EasyGoal
{
    public class Common
    {
        private Common() { }

        private static HttpContext Current
        {
            get { return HttpContext.Current; }
        }

        private static HttpRequest Request
        {
            get
            {
                if (HttpContext.Current == null)
                    throw new Exception("System.Web.HttpContext.Current is NULL.");
                return HttpContext.Current.Request;
            }
        }

        private static HttpResponse Response
        {
            get
            {
                if (HttpContext.Current == null)
                    throw new Exception("System.Web.HttpContext.Current is NULL.");
                return HttpContext.Current.Response;
            }
        }

        public static void LogException(Exception e, bool xml)
        {
            DateTime datetime = DateTime.Now;
            string logDirectory = Current.Server.MapPath("~/log/") + datetime.ToString("yyyy-MM");
            string logFilename = datetime.ToString("yyyyMMdd");
            string timeString = datetime.ToString("yyyy-MM-dd HH:mm:ss.fff");
            if (!Directory.Exists(logDirectory))
                Directory.CreateDirectory(logDirectory);

            if (xml)
            {
                string logFile = string.Format("{0}/{1}.log.xml", logDirectory, logFilename);
                LogExceptionToXML(e, timeString, logFile);
            }
            else
            {
                string logFile = string.Format("{0}/{1}.log", logDirectory, logFilename);
                LogException(e, timeString, logFile);
            }
        }

        private static void LogException(Exception e, string datetime, string logFile)
        {
            StreamWriter writer = null;
            try
            {
                writer = new StreamWriter(logFile, true, Encoding.UTF8);
                writer.WriteLine("-----------------------------------");
                writer.WriteLine(string.Format("Datetime:\t{0}", datetime));
                writer.WriteLine(string.Format("ClientIP:\t{0}", Request.UserHostAddress));
                writer.WriteLine(string.Format("ClientUA:\t{0}", Request.UserAgent));
                writer.WriteLine(string.Format("ExactURL:\t{0}", Request.Url.ToString()));
                writer.WriteLine(string.Format("Messages:\t{0}", e.Message));
                writer.WriteLine("StackTrace:");
                writer.WriteLine(e.StackTrace);
                writer.WriteLine();
            }
            finally
            {
                if (writer != null)
                    writer.Close();
            }
        }

        private static void LogExceptionToXML(Exception e, string datetime, string logFile)
        {
            XDocument document = null;
            XElement rootNode = null;

            if (!File.Exists(logFile))
            {
                document = new XDocument();
                document.Declaration = new XDeclaration("1.0", "utf-8", null);
                document.Add(new XProcessingInstruction("xml-stylesheet", "type=\"text/xsl\" href='/style/log.xsl'"));
                rootNode = new XElement("Log");
                document.Add(rootNode);
            }
            else
            {
                document = XDocument.Load(logFile);
                rootNode = document.Root;
            }

            XElement traceNode = new XElement("StackTrace");
            foreach (string line in Regex.Split(e.StackTrace, "\r\n"))
            {
                traceNode.Add(new XElement("Line", line.Trim()));
            }
            rootNode.Add(
                new XElement("Exception", new XAttribute("Time", datetime),
                    new XElement("ClientIP", Request.UserHostAddress),
                    new XElement("ClientUA", Request.UserAgent),
                    new XElement("ExactURL", Request.Url.ToString()),
                    new XElement("Message", e.Message),
                    new XElement("Source", e.Source), traceNode
                    )
                );
            document.Save(logFile);
        }

    }
}