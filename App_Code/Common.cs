using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;

using Encoder = Microsoft.Security.Application.Encoder;

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

        public static void LogException(Exception e)
        {
            string datetime = DateTime.Now.ToString();

            System.IO.StreamWriter writer = null;
            try
            {
                string path = Current.Server.MapPath("~/ErrorLog/") + DateTime.Now.ToString("yyyyMMdd");
                string filename = string.Format("L{0}_{1}.xml", DateTime.Now.ToString("HHmmss"), Guid.NewGuid().ToString("N"));

                if (!System.IO.Directory.Exists(path))
                {
                    System.IO.Directory.CreateDirectory(path);
                }
                System.IO.FileInfo file = new System.IO.FileInfo(path + "/" + filename);
                writer = new System.IO.StreamWriter(file.FullName, true, Encoding.UTF8);

                writer.WriteLine("<?xml version=\"1.0\" encoding=\"utf-8\"?>");
                writer.WriteLine("<Exception>");
                writer.WriteLine(string.Format("  <DateTime>{0}</DateTime>", datetime));
                writer.WriteLine(string.Format("  <ExactURL>{0}</ExactURL>", Request.Url.ToString()));
                writer.WriteLine(string.Format("  <Address>{0}</Address>", Request.UserHostAddress));
                writer.WriteLine(string.Format("  <Message>{0}</Message>", e.Message));
                writer.WriteLine("  <StackTrace>");
                string stackTrace = Regex.Replace(string.Format("<line>{0}</line>", Encoder.XmlEncode(e.StackTrace)), @"&#13;&#10;\s*", "</line><line>");
                writer.WriteLine(stackTrace);
                writer.WriteLine("  </StackTrace>");
                writer.WriteLine("</Exception>");
            }
            finally
            {
                if (writer != null)
                    writer.Close();
            }
        }

        public static void LogToSingleXML(Exception e)
        {

            string path = Current.Server.MapPath("~/ErrorLog/") + DateTime.Now.ToString("yyyyMMdd");
            string filename = string.Format("L{0}_{1}.xml", DateTime.Now.ToString("HHmmss"), Guid.NewGuid().ToString("N"));

            if (!System.IO.Directory.Exists(path))
            {
                System.IO.Directory.CreateDirectory(path);
            }
            XmlDocument document = new XmlDocument();
            document.AppendChild(document.CreateXmlDeclaration("1.0", "utf-8", null));

            XmlElement root = document.CreateElement("Exception");
            document.AppendChild(root);

            XmlElement datetime = document.CreateElement("Datetime");
            datetime.InnerText = DateTime.Now.ToString();
            XmlElement exactURL = document.CreateElement("ExactURL");
            exactURL.InnerText = Request.Url.ToString();
            XmlElement address = document.CreateElement("Address");
            address.InnerText = Request.UserHostAddress;
            XmlElement message = document.CreateElement("Message");
            message.InnerText = e.Message;
            XmlElement stackTrace = document.CreateElement("StackTrace");

            string[] traces = Regex.Split(e.StackTrace, @"\r\n\s*");
            foreach (string trace in traces)
            {
                XmlElement line = document.CreateElement("Line");
                line.InnerText = trace.Trim();
                stackTrace.AppendChild(line);
            }
            root.AppendChild(datetime);
            root.AppendChild(exactURL);
            root.AppendChild(address);
            root.AppendChild(message);
            root.AppendChild(stackTrace);
            document.Save(path + "/" + filename);
        }

        public static void LogToXML(Exception e)
        {

            string path = Current.Server.MapPath("~/ErrorLog/") + DateTime.Now.ToString("yyyyMM");
            string filename = string.Format("L{0}.xml", DateTime.Now.ToString("yyyyMMdd"));

            if (!System.IO.Directory.Exists(path))
            {
                System.IO.Directory.CreateDirectory(path);
            }
            XmlDocument document = new XmlDocument();
            document.AppendChild(document.CreateXmlDeclaration("1.0", "utf-8", null));

            XmlElement root = document.CreateElement("Exception");
            document.AppendChild(root);

            XmlElement datetime = document.CreateElement("Datetime");
            datetime.InnerText = DateTime.Now.ToString();
            XmlElement exactURL = document.CreateElement("ExactURL");
            exactURL.InnerText = Request.Url.ToString();
            XmlElement address = document.CreateElement("Address");
            address.InnerText = Request.UserHostAddress;
            XmlElement message = document.CreateElement("Message");
            message.InnerText = e.Message;
            XmlElement stackTrace = document.CreateElement("StackTrace");

            string[] traces = Regex.Split(e.StackTrace, @"\r\n\s*");
            foreach (string trace in traces)
            {
                XmlElement line = document.CreateElement("Line");
                line.InnerText = trace.Trim();
                stackTrace.AppendChild(line);
            }
            root.AppendChild(datetime);
            root.AppendChild(exactURL);
            root.AppendChild(address);
            root.AppendChild(message);
            root.AppendChild(stackTrace);
            document.Save(path + "/" + filename);
        }
    }
}