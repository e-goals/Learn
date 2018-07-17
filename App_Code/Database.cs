using System.Data.SqlClient;

namespace EasyGoal
{
    public class Database
    {
        private Database()
        {
        }

        public static string GetConnectionString()
        {
            return System.Configuration.ConfigurationManager.ConnectionStrings["SQL_Server"].ConnectionString;
        }

        public static SqlConnection GetConnection()
        {
            return new SqlConnection(GetConnectionString());
        }

    }
}