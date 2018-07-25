using System;
using System.Collections.Generic;
using System.IO;
using System.Management;
using System.Text.RegularExpressions;
using System.Web;
using System.Xml.Linq;

namespace EasyGoal
{
    public class Common
    {
        private Common() { }

        #region HTTP Context Properties

        public static HttpContext Current
        {
            get { return HttpContext.Current; }
        }

        public static HttpRequest Request
        {
            get
            {
                if (HttpContext.Current == null)
                    throw new Exception("System.Web.HttpContext.Current is NULL.");
                return HttpContext.Current.Request;
            }
        }

        public static HttpResponse Response
        {
            get
            {
                if (HttpContext.Current == null)
                    throw new Exception("System.Web.HttpContext.Current is NULL.");
                return HttpContext.Current.Response;
            }
        }

        #endregion HTTP Context

        public static string ByteArrayToHexString(byte[] byteArray)
        {
            if (byteArray == null)
                return string.Empty;
            System.Text.StringBuilder builder = new System.Text.StringBuilder(byteArray.Length << 1);
            for (int i = 0; i < byteArray.Length; i++)
            {
                builder.Append(byteArray[i].ToString("X2"));
            }
            return builder.ToString();
        }

        public static List<ManagementObject> GetManagementObjects(string classname)
        {
            List<ManagementObject> list = new List<ManagementObject>();
            using (ManagementClass mClass = new ManagementClass(classname))
            {
                using (ManagementObjectCollection collection = mClass.GetInstances())
                {
                    foreach (ManagementObject mObject in collection)
                    {
                        list.Add(mObject);
                    }
                }
            }
            return list;
        }

        public static string GenerateFilenameByGUID(string extension)
        {
            string filename = System.Guid.NewGuid().ToString("N");
            if (extension.StartsWith("."))
                return filename + extension;
            return string.Format("{0}.{1}", filename, extension);
        }
        public static string GenerateFilenameByTime(string extension)
        {
            string filename = EasyGoal.Datetime.Now.ToString("yyyyMMdd-HHmmss-fffffff");
            if (extension.StartsWith("."))
                return filename + extension;
            return string.Format("{0}.{1}", filename, extension);
        }

        #region ArrayToString

        public static string ArrayToString<T>(T[] array, string separator)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            for (int i = 0; i < array.Length; i++)
            {
                sb.Append(array[i]);
                if (i < array.Length - 1)
                    sb.Append(separator);
            }
            return sb.ToString();
        }

        public static string ArrayToString(string[] array, string separator)
        {
            return array != null ? ArrayToString<string>(array, separator) : "String[]_NULL";
        }

        public static string ArrayToString(byte[] array, string separator)
        {
            return array != null ? ArrayToString<Byte>(array, separator) : "Byte[]_NULL";
        }

        public static string ArrayToString(Int16[] array, string separator)
        {
            return array != null ? ArrayToString<Int16>(array, separator) : "Int16[]_NULL";
        }

        public static string ArrayToString(Int32[] array, string separator)
        {
            return array != null ? ArrayToString<Int32>(array, separator) : "Int32[]_NULL";
        }

        public static string ArrayToString(Int64[] array, string separator)
        {
            return array != null ? ArrayToString<Int64>(array, separator) : "Int64[]_NULL";
        }

        public static string ArrayToString(sbyte[] array, string separator)
        {
            return array != null ? ArrayToString<sbyte>(array, separator) : "SByte[]_NULL";
        }

        public static string ArrayToString(UInt16[] array, string separator)
        {
            return array != null ? ArrayToString<UInt16>(array, separator) : "UInt16[]_NULL";
        }

        public static string ArrayToString(UInt32[] array, string separator)
        {
            return array != null ? ArrayToString<UInt32>(array, separator) : "UInt32[]_NULL";
        }

        public static string ArrayToString(UInt64[] array, string separator)
        {
            return array != null ? ArrayToString<UInt64>(array, separator) : "UInt64[]_NULL";
        }

        #endregion ArrayToString

        #region Base64

        public static string Base64Encode(string source, System.Text.Encoding encoding)
        {
            byte[] bytes = encoding.GetBytes(source);
            string result = Convert.ToBase64String(bytes);
            return result;
        }

        public static string Base64Encode(string source)
        {
            return Base64Encode(source, System.Text.Encoding.UTF8);
        }

        public static string Base64Decode(string base64, System.Text.Encoding encoding)
        {
            byte[] bytes = Convert.FromBase64String(base64);
            string result = encoding.GetString(bytes);
            return result;
        }

        public static string Base64Decode(string base64)
        {
            return Base64Decode(base64, System.Text.Encoding.UTF8);
        }

        public static string Base64EncodeImproved(string source)
        {
            return Base64Encode(source, System.Text.Encoding.UTF8).Replace('+', '-').Replace('/', '~');
        }

        #endregion Base64

        #region Hardware & IPAddress

        public static ulong GetPMemoryTotalCapacity()
        {
            ulong capacity = 0UL;
            using (ManagementClass mClass = new ManagementClass("Win32_PhysicalMemory"))
            {
                using (ManagementObjectCollection collection = mClass.GetInstances())
                {
                    foreach (ManagementObject mObject in collection)
                    {
                        if (mObject.Properties["Capacity"].Value != null)
                            capacity += (ulong)mObject.Properties["Capacity"].Value;
                        else
                        {
                            capacity = 0UL;
                            break;
                        }
                    }
                }
            }
            return capacity;
        }

        public static ulong GetPMemoryTotalSizeByCS()
        {
            ulong capacity = 0UL;
            string hostname = System.Net.Dns.GetHostName();
            ManagementScope scope = new ManagementScope(@"ROOT\CIMV2");
            ManagementPath path = new ManagementPath(string.Format("Win32_ComputerSystem.Name=\"{0}\"", hostname));
            using (ManagementObject cs = new ManagementObject(scope, path, new ObjectGetOptions()))
            {
                if (cs.Properties["TotalPhysicalMemory"].Value != null)
                    capacity = (ulong)cs.Properties["TotalPhysicalMemory"].Value;
            }
            return capacity;
        }

        public static ulong GetPMemoryAvailableSize()
        {
            ulong capacity = 0UL;
            ManagementScope scope = new ManagementScope(@"ROOT\CIMV2");
            ManagementPath path = new ManagementPath("Win32_OperatingSystem=@");
            using (ManagementObject os = new ManagementObject(scope, path, new ObjectGetOptions()))
            {
                if (os.Properties["TotalVisibleMemorySize"].Value != null)
                    capacity = (UInt64)os.Properties["TotalVisibleMemorySize"].Value;
            }
            return capacity;
        }

        public static UInt64 GetPMemoryTotalFreeSize()
        {
            UInt64 capacity = 0UL;
            string hostname = System.Net.Dns.GetHostName();
            ManagementScope scope = new ManagementScope(@"ROOT\CIMV2");
            ManagementPath path = new ManagementPath("Win32_OperatingSystem=@");
            using (ManagementObject os = new ManagementObject(scope, path, new ObjectGetOptions()))
            {
                if (os.Properties["FreePhysicalMemory"].Value != null)
                    capacity = (UInt64)os.Properties["FreePhysicalMemory"].Value;
            }
            return capacity;
        }

        public static string GetLocationByIPAddress(string ipAddress)
        {
            if (ipAddress == "::1")
                return "Loopback Network";

            byte[] ipSections = SplitIPv4Address(ipAddress);
            if (ipSections == null)
                return "Incorrect IP Address";

            switch (ipSections[0])
            {
                case 10:
                    return "Local Area Network";
                case 127:
                    return "Loopback Network";
                case 169:
                    if (ipSections[1] == 254)
                        return "No DHCP Server";
                    else break;
                case 172:
                    if (ipSections[1] >> 4 == 1) // 16 <= ipSections[1] <= 31
                        return "Local Area Network";
                    else break;
                case 192:
                    if (ipSections[1] == 168)
                        return "Local Area Network";
                    else break;
                default:
                    break;
            }
            return "Public Network";
        }

        public static bool IsValidIpAddress(string ipString)
        {
            string pattern = @"^((?:(?:25[0-5]|2[0-4]\d|1\d{2}|[1-9]?\d)\.){3}(?:25[0-5]|2[0-4]\d|1\d{2}|[1-9]?\d))$";
            System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(pattern);
            return regex.IsMatch(ipString);
        }

        public static string FormatIpAddress(string ipAddress)
        {
            if (!IsValidIpAddress(ipAddress))
                return ipAddress;
            string[] ipSections = ipAddress.Split('.');
            for (int i = 0; i < 4; i++)
                ipSections[i] = ipSections[i].PadLeft(3, '0');
            return string.Format("{0}.{1}.{2}.{3}", ipSections[0], ipSections[1], ipSections[2], ipSections[3]);
        }

        public static string RegainIpAddress(string formattedIpAddress)
        {
            return System.Text.RegularExpressions.Regex.Replace(formattedIpAddress, @"\.0{1,2}", ".").TrimStart('0');
        }

        public static bool IsIPv4Address(string ipAddress)
        {
            string regex = @"^(?:(?:25[0-5]|2[0-4]\d|1\d{2}|[1-9]?\d)\.){4}$";
            return System.Text.RegularExpressions.Regex.IsMatch(ipAddress + ".", regex);
        }

        public static bool IsLoopbackAddress(string ipAddress)
        {
            if (ipAddress == "::1")
                return true;
            string ipv4Regex = @"^127(?:\.(?:25[0-5]|2[0-4]\d|1\d{2}|[1-9]?\d)){3}$";
            return System.Text.RegularExpressions.Regex.IsMatch(ipAddress, ipv4Regex);
        }

        public static byte[] SplitIPv4Address(string ipAddress)
        {
            byte[] sections = { 0, 0, 0, 0 };
            Regex regex = new Regex(@"^(?:(?<section>25[0-5]|2[0-4]\d|1\d{2}|[1-9]?\d)\.){4}$");
            Match match = regex.Match(ipAddress + ".");
            if (match.Success)
            {
                CaptureCollection captures = match.Groups["section"].Captures;
                for (int i = 0; i < captures.Count; i++)
                    sections[i] = byte.Parse(captures[i].Value);
            }
            else { sections = null; }
            return sections;
        }

        public static string GetMACAddressByIPAddress(string ipAddress)
        {
            if (Common.IsLoopbackAddress(ipAddress))
                return "Unknown";
            string macAddress = null;
            using (ManagementClass mClass = new ManagementClass("Win32_NetworkAdapterConfiguration"))
            {
                using (ManagementObjectCollection collection = mClass.GetInstances())
                {
                    foreach (ManagementObject mObject in collection)
                    {
                        if (!(bool)mObject.Properties["IPEnabled"].Value)
                            continue;
                        string[] ips = (string[])mObject.Properties["IPAddress"].Value;
                        for (int i = 0; i < ips.Length; i++)
                        {
                            if (ips[i] == ipAddress)
                            {
                                macAddress = (string)mObject.Properties["MACAddress"].Value;
                                break;
                            }
                        }
                        if (macAddress != null)
                            break;
                    }
                }
            }
            return macAddress;
        }

        public static string GetNetAdapterByIPAddress(string ipAddress)
        {
            if (Common.IsLoopbackAddress(ipAddress))
                return "Unknown";
            string netAdapter = null;
            using (ManagementClass mClass = new ManagementClass("Win32_NetworkAdapterConfiguration"))
            {
                using (ManagementObjectCollection collection = mClass.GetInstances())
                {
                    foreach (ManagementObject mObject in collection)
                    {
                        if (!(bool)mObject.Properties["IPEnabled"].Value)
                            continue;
                        string[] ips = (string[])mObject.Properties["IPAddress"].Value;
                        for (int i = 0; i < ips.Length; i++)
                        {
                            if (ips[i] == ipAddress)
                            {
                                netAdapter = (string)mObject.Properties["Description"].Value;
                                break;
                            }
                        }
                        if (netAdapter != null)
                            break;
                    }
                }
            }
            return netAdapter;
        }

        #endregion Hardware & IPAddress

        #region Log Exception

        public static void LogException(Exception e, bool xml)
        {
            DateTime datetime = EasyGoal.Datetime.Now;
            string logDirectory = Current.Server.MapPath("~/log/") + datetime.ToString("yyyy-MM");
            string logFilename = datetime.ToString("yyyyMMdd");
            string timeString = datetime.ToString("yyyy-MM-dd HH:mm:ss.fffffff");
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
                writer = new StreamWriter(logFile, true, System.Text.Encoding.UTF8);
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
                new XElement("Exception",
                    new XAttribute("Time", datetime),
                    new XAttribute("Type", e.GetType()),
                    new XElement("ClientIP", Request.UserHostAddress),
                    new XElement("ClientUA", Request.UserAgent),
                    new XElement("ExactURL", Request.Url.ToString()),
                    new XElement("Message", e.Message),
                    new XElement("Source", e.Source), traceNode
                    )
                );
            document.Save(logFile);
        }

        #endregion Log Exception

        #region Message

        public static void Message(System.Web.UI.Page page, string message)
        {
            System.Web.UI.ClientScriptManager manager = page.ClientScript;
            if (manager.IsStartupScriptRegistered("MESSAGE"))
                return;
            string script = string.Format("alert(\"{0}\");", message);
            manager.RegisterStartupScript(page.GetType(), "MESSAGE", "\t" + script + "\n", true);
        }

        public static void Message(System.Web.UI.Page page, string message, bool reload)
        {
            if (!reload)
            {
                Message(page, message);
                return;
            }

            System.Web.UI.ClientScriptManager manager = page.ClientScript;
            if (manager.IsStartupScriptRegistered("MESSAGE"))
                return;
            string script = string.Format("alert(\"{0}\"); location.replace(location.href)", message);
            manager.RegisterStartupScript(page.GetType(), "MESSAGE", "\t" + script + "\n", true);
        }

        public static void Message(System.Web.UI.Page page, string message, string newURL)
        {
            System.Web.UI.ClientScriptManager manager = page.ClientScript;
            if (manager.IsStartupScriptRegistered("MESSAGE"))
                return;
            string script = string.Format("alert(\"{0}\"); location.href = \"{1}\";", message, newURL);
            manager.RegisterStartupScript(page.GetType(), "MESSAGE", "\t" + script + "\n", true);
        }

        #endregion Message

        #region Parameters of URL

        public static System.Int16 GetParamValueAsNumber(string paramName, System.Int16 error)
        {
            System.Int16 result = error;
            try
            {
                result = System.Int16.Parse(Request.QueryString[paramName]);
            }
            catch
            {
            }
            return result;
        }

        public static System.Int32 GetParamValueAsNumber(string paramName, System.Int32 error)
        {
            System.Int32 result = error;
            try
            {
                result = System.Int32.Parse(Request.QueryString[paramName]);
            }
            catch
            {
            }
            return result;
        }

        public static System.Int64 GetParamValueAsNumber(string paramName, System.Int64 error)
        {
            System.Int64 result = error;
            try
            {
                result = System.Int64.Parse(Request.QueryString[paramName]);
            }
            catch
            {
            }
            return result;
        }

        public static System.Int16 GetParamValueAsNumber(System.Web.UI.Page page, string paramName, System.Int16 error)
        {
            System.Int16 result = error;
            try
            {
                result = System.Int16.Parse(page.Request.QueryString[paramName]);
            }
            catch
            {
            }
            return result;
        }

        public static System.Int32 GetParamValueAsNumber(System.Web.UI.Page page, string paramName, System.Int32 error)
        {
            System.Int32 result = error;
            try
            {
                result = System.Int32.Parse(page.Request.QueryString[paramName]);
            }
            catch
            {
            }
            return result;
        }

        public static System.Int64 GetParamValueAsNumber(System.Web.UI.Page page, string paramName, System.Int64 error)
        {
            System.Int64 result = error;
            try
            {
                result = System.Int64.Parse(page.Request.QueryString[paramName]);
            }
            catch
            {
            }
            return result;
        }

        public static string GetParamValue(string paramName)
        {
            return Request.QueryString[paramName];
        }

        public static string GetParamValue(System.Web.UI.Page page, string paramName)
        {
            return page.Request.QueryString[paramName];
        }

        public static string GetParamValue(string paramName, System.Text.Encoding encoding)
        {
            System.Collections.Specialized.NameValueCollection queryStrings
                = System.Web.HttpUtility.ParseQueryString(Request.Url.Query.Substring(1), encoding);
            if (queryStrings[paramName] == null)
            {
                return null;
            }
            return queryStrings[paramName].ToString();
        }

        public static string GetParamValue(System.Web.UI.Page page, string paramName, System.Text.Encoding encoding)
        {
            System.Collections.Specialized.NameValueCollection queryStrings
                = System.Web.HttpUtility.ParseQueryString(page.Request.Url.Query.Substring(1), encoding);
            if (queryStrings[paramName] == null)
            {
                return null;
            }
            return queryStrings[paramName].ToString();
        }

        #endregion Parameters of URL

    }
}