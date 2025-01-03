using System;


namespace PatientDetails_Entities
{
    public class PatientDetailEntities : PatientDetailResult
    {
        public int ID { get; set; }
        public DateTime ModifiedDate { get; set; }
        public Decimal Dosage { get; set; }
        public string Drug { get; set; }
        public string Patient { get; set; }
        public bool Deleted_At { get; set; }
    }

    public class PatientDetailResult
    {
        public bool IsSuccess { get; set; }
        public bool IsListResult { get; set; }
        public string Result { get; set; }
    }
}