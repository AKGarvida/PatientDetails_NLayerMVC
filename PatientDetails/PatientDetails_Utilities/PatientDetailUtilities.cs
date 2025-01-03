using System;


namespace PatientDetails_Utilities
{
    public static class PatientDetailUtilities
    {
        public static readonly string Required_Message = "All field/s are required.";
        public static readonly string Error_Message = "Record already exists.";
        public static readonly string Duplicate_Error_Message = "Cannot add same drug to a patient.";
        public static readonly string Delete_Failure_Message = "Failure to delete patient record.";
        public static readonly string Create_Failure_Message = "Failure to create a patient record.";

        public static readonly string Create_Success_Message = "Record successfully saved.";
        public static readonly string Update_Success_Message = "Record successfully updated.";
        public static readonly string Delete_Success_Message = "Record successfully deleted.";
    }
}