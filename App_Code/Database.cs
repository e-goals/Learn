using System.Configuration;
using System.Data.SqlClient;

namespace EZGoal
{
    public static class Database
    {
        public static string ConnectionString { get; private set; }
        public static string Version { get; private set; }
        public static string VersionMajor { get; private set; }
        public static string VersionMinor { get; private set; }
        public static string VersionBuild { get; private set; }
        public static string VersionRevision { get; private set; }

        static Database()
        {
            ConnectionString = ConfigurationManager.ConnectionStrings["SQL_Server"].ConnectionString;

            SqlConnection connection = new SqlConnection(ConnectionString);
            connection.Open();
            Version = connection.ServerVersion;
            connection.Close();
            connection.Dispose();

            string[] vDetails = Version.Split('.');
            VersionMajor = vDetails[0];
            VersionMinor = vDetails[1];
            VersionBuild = vDetails[2];
            VersionRevision = (vDetails.Length > 3) ? vDetails[3] : "";
        }

        public static SqlConnection GetConnection()
        {
            return new SqlConnection(ConnectionString);
        }

    }
}