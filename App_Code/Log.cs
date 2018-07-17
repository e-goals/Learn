using Microsoft.ApplicationBlocks.Data;
using System;
using System.Data;
using System.Data.SqlClient;

namespace EasyGoal
{
    public class Log
    {
        private static System.Web.HttpContext Current
        {
            get
            {
                return System.Web.HttpContext.Current;
            }
        }

        private static System.Web.HttpRequest Request
        {
            get
            {
                if (System.Web.HttpContext.Current == null)
                    throw new System.Exception("System.Web.HttpContext.Current is NULL.");
                return System.Web.HttpContext.Current.Request;
            }
        }

        private static System.Web.HttpResponse Response
        {
            get
            {
                if (System.Web.HttpContext.Current == null)
                    throw new System.Exception("System.Web.HttpContext.Current is NULL.");
                return System.Web.HttpContext.Current.Response;
            }
        }

        private int id;
        public int Id
        {
            get { return this.id; }
            set { this.id = value; }
        }
        private DateTime timestamp;
        public DateTime Timestamp
        {
            get { return this.timestamp; }
            set { this.timestamp = value; }
        }
        private decimal timetaken;
        public decimal Timetaken
        {
            get { return this.timetaken; }
            set { this.timetaken = value; }
        }
        private int length;
        public int Length
        {
            get { return this.length; }
            set { this.length = value; }
        }
        private string method;
        public string Method
        {
            get { return this.method; }
            set { this.method = value; }
        }
        private string screen;
        public string Screen
        {
            get { return this.screen; }
            set { this.screen = value; }
        }
        private string filePath;
        public string FilePath
        {
            get { return this.filePath; }
            set { this.filePath = value; }
        }
        private string requestURL;
        public string RequestURL
        {
            get { return this.requestURL; }
            set { this.requestURL = value; }
        }
        private string referrer;
        public string Referrer
        {
            get { return this.referrer; }
            set { this.referrer = value; }
        }
        private string userAgent;
        public string UserAgent
        {
            get { return this.userAgent; }
            set { this.userAgent = value; }
        }
        private string userHost;
        public string UserHost
        {
            get { return this.userHost; }
            set { this.userHost = value; }
        }
        private string userPort;
        public string UserPort
        {
            get { return this.userPort; }
            set { this.userPort = value; }
        }
        private string httpDNT;
        public string HTTP_DNT
        {
            get { return this.httpDNT; }
            set { this.httpDNT = value; }
        }
        private string httpVia;
        public string HTTP_Via
        {
            get { return this.httpVia; }
            set { this.httpVia = value; }
        }
        private string httpXFF;
        public string HTTP_X_Forwarded_For
        {
            get { return this.httpXFF; }
            set { this.httpXFF = value; }
        }
        private int statusC;
        public int StatusCode
        {
            get { return this.statusC; }
            set { this.statusC = value; }
        }
        private string statusD;
        public string StatusDescription
        {
            get { return this.statusD; }
            set { this.statusD = value; }
        }

        private Log() { }

        public Log(int id)
        {
            this.id = id;
        }

        public Log(decimal timetaken, string screenSize)
        {
            this.timestamp = Current.Timestamp;
            this.timetaken = timetaken;
            this.length = Request.TotalBytes;
            this.method = Request.HttpMethod;
            this.screen = screenSize;
            this.filePath = Request.FilePath;
            this.requestURL = Request.Url.ToString();
            this.referrer = Request.UrlReferrer == null ? "" : Request.UrlReferrer.ToString();
            this.userAgent = Request.UserAgent;
            this.userHost = Request.UserHostAddress;
            this.userPort = Request.ServerVariables["REMOTE_PORT"];
            this.httpDNT = Request.ServerVariables["HTTP_DNT"];
            this.httpVia = Request.ServerVariables["HTTP_Via"];
            this.httpXFF = Request.ServerVariables["HTTP_X_Forwarded_For"];
            this.statusC = Response.StatusCode;
            this.statusD = Response.StatusDescription;
        }

        #region Insert, Delete & Update methods

        public bool Insert()
        {
            string cmdText = "INSERT INTO [Log] ([LOAD_BEGIN_TIME], [LOAD_TIME_TAKEN], [LENGTH], [METHOD], [SCREEN], [FILEPATH], [REQUEST_URL], [REFERRER], [USER_AGENT], [USER_HOST], [USER_PORT], [HTTP_DNT], [HTTP_VIA], [HTTP_XFF], [STATUS_C], [STATUS_D]) VALUES (@LoadBeginTime, @LoadTimeTaken, @Length, @Method, @Screen, @FilePath, @RequestURL, @Referrer, @UserAgent, @UserHost, @UserPort, @HTTP_DNT, @HTTP_Via, @HTTP_XFF, @Status_C, @Status_D)";
            SqlParameter[] parameters = new SqlParameter[] { 
                new SqlParameter("@LoadBeginTime", this.timestamp),
                new SqlParameter("@LoadTimeTaken", this.timetaken),
                new SqlParameter("@Length", this.length),
                new SqlParameter("@Method", this.method),
                new SqlParameter("@Screen", this.screen),
                new SqlParameter("@FilePath", this.filePath),
                new SqlParameter("@RequestURL", this.requestURL),
                new SqlParameter("@Referrer", this.referrer),
                new SqlParameter("@UserAgent", this.userAgent),
                new SqlParameter("@UserHost", this.userHost),
                new SqlParameter("@UserPort", this.userPort),
                new SqlParameter("@HTTP_DNT", this.httpDNT),
                new SqlParameter("@HTTP_VIa", this.httpVia),
                new SqlParameter("@HTTP_XFF", this.httpXFF),
                new SqlParameter("@Status_C", this.statusC),
                new SqlParameter("@Status_D", this.statusD)
            };
            SqlConnection connection = Database.GetConnection();
            int result = SqlHelper.ExecuteNonQuery(connection, CommandType.Text, cmdText, parameters);
            connection.Close();
            connection.Dispose();
            return (result > 0);
        }

        public bool Delete()
        {
            string cmdText = "DELETE FROM [News] WHERE [RecordID] = @Id";
            SqlParameter parameter = new SqlParameter("@Id", this.id);
            SqlConnection connection = Database.GetConnection();
            int result = SqlHelper.ExecuteNonQuery(connection, CommandType.Text, cmdText, parameter);
            connection.Close();
            connection.Dispose();
            return (result > 0);
        }

        #endregion Insert, Delete & Update methods

    }
}