using MySql.Data.MySqlClient;
using System.Data;

namespace BookAHotel.Data
{
    public class DapperContext
    {
        private readonly string _connectionString;

        public DapperContext()
        {
            _connectionString = "Server=localhost;Database=hotelbooking;User=root;Password=Munmeo0503.;";
        }

        public IDbConnection CreateConnection()
            => new MySqlConnection(_connectionString);
    }
}
