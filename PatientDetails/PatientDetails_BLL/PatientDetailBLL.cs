using System;
using System.Collections.Generic;
using PatientDetails_DAL.Service;
using PatientDetails_Entities;

namespace PatientDetails_BLL
{
    public class PatientDetailBLL
    {
        private readonly PatientRecordDAL _patientRecordDal;

        public PatientDetailBLL()
        {
            _patientRecordDal = new PatientRecordDAL();
        }

        public PatientDetailEntities CreatePatient(PatientDetailEntities patient)
        {
            ValidatePatientDetails(patient);

            try
            {
                return _patientRecordDal.CreatePD(patient);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in CreatePatient method: {ex.Message}");
                throw;
            }
        }

        public List<PatientDetailEntities> GetPatients(string patientName = null, string drug = null, decimal? dosage = null, DateTime? modifiedDate = null)
        {
            try
            {
                return _patientRecordDal.GetPatients(patientName, drug, dosage, modifiedDate);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in GetPatients method: {ex.Message}");
                throw;
            }
        }

        private static void ValidatePatientDetails(PatientDetailEntities patient)
        {
            if (patient == null)
            {
                throw new ArgumentNullException(nameof(patient), "Patient object cannot be null.");
            }

            if (string.IsNullOrWhiteSpace(patient.Drug))
            {
                throw new ArgumentException("Drug field cannot be empty.", nameof(patient.Drug));
            }

            if (string.IsNullOrWhiteSpace(patient.Patient))
            {
                throw new ArgumentException("Patient field cannot be empty.", nameof(patient.Patient));
            }

            if (patient.Dosage <= 0)
            {
                throw new ArgumentException("Dosage must be greater than zero.", nameof(patient.Dosage));
            }
        }

        public PatientDetailEntities UpdatePatient(PatientDetailEntities patient)
        {
            ValidatePatientDetails(patient);

            try
            {
                return _patientRecordDal.UpdatePD(patient);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in UpdatePatient method: {ex.Message}");
                throw;
            }
        }



        public bool DeletePatient(int id)
        {
            try
            {
                return _patientRecordDal.DeletePatient(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in DeletePatient method: {ex.Message}");
                throw;
            }
        }
    }
}
