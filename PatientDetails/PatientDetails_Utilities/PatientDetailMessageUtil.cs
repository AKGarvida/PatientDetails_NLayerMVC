using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatientDetails_Utilities
{
    public class PatientDetailMessageUtil
    {
        public static readonly string Required_Message = "All field/s are required.";
        public static readonly string Error_Message = "Record already exists.";
        public static readonly string Duplicate_Error_Message = "Cannot add same drug to a patient.";

        public static readonly string Create_Success_Message = "Record successfully saved.";
        public static readonly string Update_Success_Message = "Record successfully updated.";
        public static readonly string Delete_Success_Message = "Record successfully deleted.";
    }
}
