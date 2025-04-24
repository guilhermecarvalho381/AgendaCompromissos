using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgendaCompromissos.DAL
{
   public class Banco
    {
        private static string connectionString = @"Server=localhost;Database=AgendaDB;Trusted_Connection=True;";
        

        public static SqlConnection GetConnection()
        {
            return new SqlConnection(connectionString);
        }
    }
}
