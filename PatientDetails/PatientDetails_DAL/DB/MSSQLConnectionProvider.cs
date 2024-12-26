using System;
using System.Configuration;
using PatientDetails_DAL.DB;


namespace PatientDetails_DAL.DB
{
    public static class MSSQLConnectionProvider
    {
        private static readonly string connString;
        public static readonly int connTimeout = 60;

        static MSSQLConnectionProvider()
        {
            var connSetting = ConfigurationManager.ConnectionStrings["PatientDetails"];
            connString = connSetting.ConnectionString;
        }

        public static string GetConnectionString()
        {
            return connString;
        }

        public static int GetConnectionTimeout()
        {
            return connTimeout;
        }
    }
}
