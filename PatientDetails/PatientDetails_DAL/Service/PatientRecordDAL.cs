using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using PatientDetails_Entities;
using PatientDetails_DAL.DB;

namespace PatientDetails_DAL.Service
{
    public class PatientRecordDAL
    {
        /// Fetches a list of patients based on optional filter parameters.
        public List<PatientDetailEntities> GetPatients(string patientName = null, string drug = null, decimal? dosage = null, DateTime? modifiedDate = null)
        {
            var patients = new List<PatientDetailEntities>();

            try
            {
                using (SqlConnection conn = new SqlConnection(MSSQLConnectionProvider.GetConnectionString()))
                {
                    using (SqlCommand cmd = new SqlCommand("spPatientList", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandTimeout = MSSQLConnectionProvider.GetConnectionTimeout();

                        // Add parameters to the stored procedure
                        cmd.Parameters.Add("@Patient", SqlDbType.VarChar, 50).Value = (object)patientName ?? DBNull.Value;
                        cmd.Parameters.Add("@Drug", SqlDbType.VarChar, 50).Value = (object)drug ?? DBNull.Value;
                        cmd.Parameters.Add("@Dosage", SqlDbType.Decimal).Value = (object)dosage ?? DBNull.Value;
                        cmd.Parameters.Add("@ModifiedDate", SqlDbType.Date).Value = (object)modifiedDate ?? DBNull.Value;

                        conn.Open();

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var patient = new PatientDetailEntities
                                {
                                    ID = reader.GetInt32(reader.GetOrdinal("ID")),
                                    Dosage = reader.GetDecimal(reader.GetOrdinal("Dosage")),
                                    Drug = reader.GetString(reader.GetOrdinal("Drug")),
                                    Patient = reader.GetString(reader.GetOrdinal("Patient")),
                                    ModifiedDate = reader.GetDateTime(reader.GetOrdinal("ModifiedDate")),
                                    Deleted_At = reader.GetBoolean(reader.GetOrdinal("Deleted_At"))
                                };

                                patients.Add(patient);
                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("SQL Error: " + ex.Message);
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                throw;
            }

            return patients;
        }

        /// Inserts a new patient record into the database.
        public PatientDetailEntities CreatePD(PatientDetailEntities c)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(MSSQLConnectionProvider.GetConnectionString()))
                {
                    using (SqlCommand cmd = new SqlCommand("spPatientCreate", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandTimeout = MSSQLConnectionProvider.GetConnectionTimeout();

                        // Parameters
                        cmd.Parameters.Add("@Dosage", SqlDbType.Decimal).Value = c.Dosage;
                        cmd.Parameters.Add("@Drug", SqlDbType.VarChar, 50).Value = c.Drug;
                        cmd.Parameters.Add("@Patient", SqlDbType.VarChar, 50).Value = c.Patient;

                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
                return c;
            }
            catch (SqlException ex)
            {
                Console.WriteLine("SQL Error: " + ex.Message);
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                throw;
            }
        }

        public PatientDetailEntities UpdatePD(PatientDetailEntities c)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(MSSQLConnectionProvider.GetConnectionString()))
                {
                    using (SqlCommand cmd = new SqlCommand("spPatientRecordUpdate", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@ID", SqlDbType.Int).Value = c.ID;
                        cmd.Parameters.Add("@Dosage", SqlDbType.Decimal).Value = c.Dosage;
                        cmd.Parameters.Add("@Drug", SqlDbType.VarChar, 50).Value = c.Drug;
                        cmd.Parameters.Add("@Patient", SqlDbType.VarChar, 50).Value = c.Patient;

                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
                return c; // Ensure the updated patient object is returned
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in UpdatePD: {ex.Message}");
                throw;
            }
        }


        public bool DeletePatient(int id)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(MSSQLConnectionProvider.GetConnectionString()))
                {
                    using (SqlCommand cmd = new SqlCommand("spPatientRecordDelete", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandTimeout = MSSQLConnectionProvider.GetConnectionTimeout();

                        cmd.Parameters.Add("@ID", SqlDbType.Int).Value = id;

                        conn.Open();
                        int rowsAffected = cmd.ExecuteNonQuery();

                        return rowsAffected > 0;
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("SQL Error: " + ex.Message);
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                throw;
            }
        }
    }
}