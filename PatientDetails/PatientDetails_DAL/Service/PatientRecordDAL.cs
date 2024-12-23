using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PatientDetails_Entities;
using System.Data.SqlClient;
using PatientDetails_DAL.DB;
using System.Data;

namespace PatientDetails_DAL.Service
{
    public class PatientRecordDAL
    {
        public PatientDetailEntities CreatePD(PatientDetailEntities c)  
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(MSSQLConnectionProvider.GetConnectionString()))
                {
                    using (SqlCommand cmd = new SqlCommand("spCreateNewPatient", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandTimeout = MSSQLConnectionProvider.GetConnectionTimeout();

                        // Add parameters to the command
                        cmd.Parameters.Add("@Dosage", SqlDbType.Decimal).Value = c.Dosage;
                        cmd.Parameters.Add("@Drug", SqlDbType.VarChar, 50).Value = c.Drug;
                        cmd.Parameters.Add("@Patient", SqlDbType.VarChar, 50).Value = c.Patient;

                        // Open the connection and execute the stored procedure
                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }
                }

                // Return the patient entity after the operation
                return c;
            }
            catch (SqlException ex)
            {
                //Log SQL exception
                Console.WriteLine("SQL Error: " + ex.Message);
                throw;
            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine("Error: " + ex.Message);
                throw;
            }
        }
    }
}
