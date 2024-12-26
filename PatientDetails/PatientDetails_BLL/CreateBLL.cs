using System;
using PatientDetails_DAL.Service;
using PatientDetails_Entities;

namespace PatientDetails_BLL
{
    public class CreateBLL
    {
        private readonly PatientRecordDAL _patientRecordDal;

        public CreateBLL()
        {
            _patientRecordDal = new PatientRecordDAL();
        }

        /// Validates the patient data and insertion database to the DAL layer.
        public PatientDetailEntities CreatePatient(PatientDetailEntities patient)
        {
            ValidatePatientDetails(patient);

            try
            {
                // Delegate to DAL for database operation
                return _patientRecordDal.CreatePD(patient);
            }
            catch (Exception ex)
            {
                // Log exception
                Console.WriteLine($"Error in CreatePatient method: {ex.Message}");
                throw; 
            }
        }

        /// Validates the patient details for required fields and valid data.
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
    }
}