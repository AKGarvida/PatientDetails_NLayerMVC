﻿using System;
using System.Collections.Generic;
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

        // Method to fetch all patients
        public List<PatientDetailEntities> GetPatients(string patientName = null, string drug = null, decimal? dosage = null)
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

                        // Parameters
                        cmd.Parameters.Add("@Patient", SqlDbType.VarChar, 50).Value = (object)patientName ?? DBNull.Value;
                        cmd.Parameters.Add("@Drug", SqlDbType.VarChar, 50).Value = (object)drug ?? DBNull.Value;
                        cmd.Parameters.Add("@Dosage", SqlDbType.Decimal).Value = (object)dosage ?? DBNull.Value;

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
    }
}