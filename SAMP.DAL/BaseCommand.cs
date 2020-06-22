using System;
using System.Data;
using System.Data.SqlClient;

namespace SAMP.DAL
{
    public class BaseCommand : IDisposable
    {
        protected IDbConnection con;
        public BaseCommand()
        {
            string connectionString = "Data Source=THANUJABALAJI;Initial Catalog=SAMP;Integrated Security=True";
            con = new SqlConnection(connectionString);
        }

        public void Dispose()
        {
            //throw new NotImplementedException();
        }
    }
}
