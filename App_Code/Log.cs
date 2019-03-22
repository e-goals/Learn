using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using DBHelper = Microsoft.ApplicationBlocks.Data.SqlHelper;

namespace EZGoal
{
    public class Log
    {
        private int id;
        public int Id
        {
            get { return this.id; }
        }
        private DateTime timestamp;
        public DateTime Timestamp
        {
            get { return this.timestamp; }
        }
        private decimal timetaken;
        public decimal Timetaken
        {
            get { return this.timetaken; }
        }
        private string exactURL;
        public string ExactURL
        {
            get { return this.exactURL; }
        }
        private string filePath;
        public string FilePath
        {
            get { return this.filePath; }
        }
        private string method;
        public string Method
        {
            get { return this.method; }
        }
        private string userAgent;
        public string UserAgent
        {
            get { return this.userAgent; }
        }
        private string userHost;
        public string UserHost
        {
            get { return this.userHost; }
        }
        private string userPort;
        public string UserPort
        {
            get { return this.userPort; }
        }
        private string httpDNT;
        public string HTTP_DNT
        {
            get { return this.httpDNT; }
        }
        private string httpVia;
        public string HTTP_Via
        {
            get { return this.httpVia; }
        }
        private string httpXFF;
        public string HTTP_X_Forwarded_For
        {
            get { return this.httpXFF; }
        }
        private string referrer;
        public string Referrer
        {
            get { return this.referrer; }
        }
        private int statusCode;
        public int StatusCode
        {
            get { return this.statusCode; }
        }
        private string statusText;
        public string StatusText
        {
            get { return this.statusText; }
        }

        private Log() { }

        public Log(decimal timetaken)
        {
            this.timestamp = Common.Current.Timestamp;
            this.timetaken = timetaken;
            this.exactURL = Common.Request.Url.ToString();
            this.filePath = Common.Request.FilePath;
            this.method = Common.Request.HttpMethod;
            this.userAgent = Common.Request.UserAgent;
            this.userHost = Common.Request.UserHostAddress;
            this.userPort = Common.Request.ServerVariables["REMOTE_PORT"];
            this.httpDNT = Common.Request.ServerVariables["HTTP_DNT"];
            this.httpVia = Common.Request.ServerVariables["HTTP_Via"];
            this.httpXFF = Common.Request.ServerVariables["HTTP_X_Forwarded_For"];
            this.referrer = Common.Request.UrlReferrer == null ? null : Common.Request.UrlReferrer.ToString();
            this.statusCode = Common.Response.StatusCode;
            this.statusText = Common.Response.StatusDescription;
        }

        public bool Write()
        {
            string cmdText = "INSERT INTO [Log] ([TIMESTAMP], [TIMETAKEN], [EXACTURL], [FILEPATH], [METHOD], [USERAGENT], [USERHOST], [USERPORT], [HTTP_DNT], [HTTP_VIA], [HTTP_XFF], [REFERRER], [STATUS_CODE], [STATUS_TEXT]) ";
            cmdText += " VALUES (@Timestamp, @Timetaken, @ExactURL, @FilePath, @Method, @UserAgent, @UserHost, @UserPort, @HTTP_DNT, @HTTP_Via, @HTTP_XFF, @Referrer, @StatusCode, @StatusText)";

            SqlParameter[] parameters = new SqlParameter[] { 
                new SqlParameter("@Timestamp", this.timestamp) {
                    SqlDbType = int.Parse(EZGoal.Database.VersionMajor) > 9 ? SqlDbType.DateTime2 : SqlDbType.DateTime 
                },
                new SqlParameter("@Timetaken", this.timetaken) { SqlDbType = SqlDbType.Decimal },
                new SqlParameter("@ExactURL", this.exactURL) { Size = 512 },
                new SqlParameter("@FilePath", this.filePath) { Size = 256 },
                new SqlParameter("@Method", this.method) { Size = 8 },
                new SqlParameter("@UserAgent", this.userAgent) { Size = 256 },
                new SqlParameter("@UserHost", this.userHost) { Size = 64 },
                new SqlParameter("@UserPort", this.userPort) { Size = 8 },
                new SqlParameter("@HTTP_DNT", this.httpDNT) { Size = 8 },
                new SqlParameter("@HTTP_Via", this.httpVia) { Size = 256 },
                new SqlParameter("@HTTP_XFF", this.httpXFF) { Size = 256 },
                new SqlParameter("@Referrer", this.referrer) { Size = 512 },
                new SqlParameter("@StatusCode", this.statusCode) { SqlDbType = SqlDbType.Int },
                new SqlParameter("@StatusText", this.statusText) { Size = 128 }
            };
            SqlConnection connection = Database.GetConnection();
            int result = DBHelper.ExecuteNonQuery(connection, CommandType.Text, cmdText, parameters);
            connection.Close();
            connection.Dispose();
            return (result > 0);
        }

        public bool Erase()
        {
            return Erase(this.id);
        }

        public static bool Erase(int id)
        {
            string cmdText = "DELETE FROM [Log] WHERE [ID] = @Id";
            SqlParameter parameter = new SqlParameter("@Id", id);
            SqlConnection connection = Database.GetConnection();
            int result = DBHelper.ExecuteNonQuery(connection, CommandType.Text, cmdText, parameter);
            connection.Close();
            connection.Dispose();
            return (result > 0);
        }

        private static Log PopulateEntity(IDataRecord dr)
        {
            var log = new Log();
            log.id = Convert.ToInt32(dr["ID"]);
            log.timestamp = Convert.ToDateTime(dr["TIMESTAMP"]);
            log.timetaken = Convert.ToDecimal(dr["TIMETAKEN"]);
            log.exactURL = dr["EXACTURL"].ToString();
            log.filePath = dr["FILEPATH"].ToString();
            log.method = dr["METHOD"].ToString();
            log.userAgent = dr["USERAGENT"].ToString();
            log.userHost = dr["USERHOST"].ToString();
            log.userPort = dr["USERPORT"].ToString();
            log.httpDNT = dr["HTTP_DNT"].Equals(DBNull.Value) ? null : dr["HTTP_DNT"].ToString();
            log.httpVia = dr["HTTP_VIA"].Equals(DBNull.Value) ? null : dr["HTTP_VIA"].ToString();
            log.httpXFF = dr["HTTP_XFF"].Equals(DBNull.Value) ? null : dr["HTTP_XFF"].ToString();
            log.referrer = dr["REFERRER"].Equals(DBNull.Value) ? null : dr["REFERRER"].ToString();
            log.statusCode = Convert.ToInt32(dr["STATUS_CODE"]);
            log.statusText = dr["STATUS_TEXT"].ToString();
            return log;
        }

        public static List<Log> GetAll()
        {
            var list = new List<Log>();
            string sqlText = "SELECT * FROM [Log] ORDER BY [ID] DESC";
            using (SqlConnection connection = Database.GetConnection())
            {
                SqlDataReader dr = DBHelper.ExecuteReader(connection, CommandType.Text, sqlText);
                while (dr.Read())
                {
                    list.Add(PopulateEntity(dr));
                }
            }
            return list;
        }

        public static List<Log> GetTop(int count)
        {
            var list = new List<Log>();
            string sqlText = "SELECT TOP (@Count) * FROM [Log] ORDER BY [ID] DESC";
            SqlParameter parameter = new SqlParameter("@Count", count);
            using (SqlConnection connection = Database.GetConnection())
            {
                SqlDataReader dr = DBHelper.ExecuteReader(connection, CommandType.Text, sqlText, parameter);
                while (dr.Read())
                {
                    list.Add(PopulateEntity(dr));
                }
            }
            return list;
        }

        public static List<Log> GetAllError()
        {
            var list = new List<Log>();
            string sqlText = "SELECT * FROM [Log] WHERE [STATUS_CODE] != '200' ORDER BY [ID] DESC";
            using (SqlConnection connection = Database.GetConnection())
            {
                SqlDataReader dr = DBHelper.ExecuteReader(connection, CommandType.Text, sqlText);
                while (dr.Read())
                {
                    list.Add(PopulateEntity(dr));
                }
            }
            return list;
        }

    }
}