using Data;

namespace WebAPI.Helpers
{
    public class ConnectionString : IConnectionString
    {
        public string SqlConnectionString { get; private set; }

        public ConnectionString(string conenctionString)
        {
            SqlConnectionString = conenctionString;
        }
    }
}
