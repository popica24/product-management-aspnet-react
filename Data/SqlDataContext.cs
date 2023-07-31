using Microsoft.Data.SqlClient;

namespace Data
{
    public class SqlDataContext : IDisposable
    {
        public SqlConnection Connection { get; set; }

        public SqlDataContext(IConnectionString connectionString)
        {
            Connection = new SqlConnection(connectionString.SqlConnectionString);
            Connection.Open();
        }

        public void Dispose()
        {
            Connection.Close();
            Connection.Dispose();   
        }
    }
}
