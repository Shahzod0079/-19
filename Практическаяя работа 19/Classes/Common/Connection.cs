using MySql.Data.MySqlClient;
using System.Configuration;

namespace Практическая_19.Classes.Common
{
    public class Connection
    {
        private static string connectionString =
            ConfigurationManager.ConnectionStrings["MySQLConnection"].ConnectionString;

        public static MySqlConnection GetConnection()
        {
            return new MySqlConnection(connectionString);
        }
    }
}