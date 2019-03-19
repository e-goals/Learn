using System.Configuration;
using System.Data.SqlClient;

namespace EZGoal
{
    public class Database
    {
        private Database() { }

        public static string ConnectionString
        {
            get { return ConfigurationManager.ConnectionStrings["SQL_Server"].ConnectionString; }
        }

        public static SqlConnection GetConnection()
        {
            return new SqlConnection(ConnectionString);
        }

    }
}