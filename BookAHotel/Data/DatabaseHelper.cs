using MySql.Data.MySqlClient;
using System.Data;

namespace BookAHotel.Data
{
    public class DatabaseHelper
    {
        private readonly string _connectionString;

        public DatabaseHelper(string connectionString)
        {
            _connectionString = connectionString;
        }
        
        public MySqlDataReader RunSqlQuery(string query)
        {
            using (var dbConn = new MySqlConnection(_connectionString))
            {
                dbConn.Open();
                using (var cmd = dbConn.CreateCommand())
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = query;

                    return cmd.ExecuteReader();
                }
            }
        }
    }

}
