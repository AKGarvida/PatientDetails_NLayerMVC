using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace PatientDetails_Entities
{
    public class PatientDetailEntities
    {
        public int ID { get; set; }
        public DateTime ModifiedDate { get; set; }
        [Required(ErrorMessage = "Please fill the content area")]
        public double Dosage { get; set; }
        [Required(ErrorMessage = "Please fill the content area")]
        public string Drug { get; set; }
        [Required(ErrorMessage = "Please fill the content area")]
        public string Patient { get; set; }
        public bool Deleted_At { get; set; }
    }
}
