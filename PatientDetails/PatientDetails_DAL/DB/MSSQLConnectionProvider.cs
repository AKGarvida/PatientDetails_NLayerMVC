using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatientDetails_DAL.DB
{
    public static class MSSQLConnectionProvider
    {
        public static string GetConnectionString()
        {
            // SQL Server Details
            string server = "MSI-EDISONIII\\SQLEXPRESS";
            string database = "PatientDetails";
            string user = "asherkairos";
            string password = "Asher.kairos0922";

            // Build the connection string
            return $"Server={server};Database={database};User Id={user};Password={password};";
        }
        
        public static int GetConnectionTimeout()
        {
            return 60;
        }
    }
}
