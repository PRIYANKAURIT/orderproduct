using System.Data;
using System.Data.SqlClient;

namespace orderproduct.Context
{
    public class DapperContext
    {
        private readonly IConfiguration _configuration;
        private readonly string connectionString;

        public DapperContext(IConfiguration configuration)
        {
            _configuration = configuration;
           connectionString = _configuration.GetConnectionString("SqlConnection");
        }

        public IDbConnection CreateConnection()
            => new SqlConnection(connectionString);
    }
}
